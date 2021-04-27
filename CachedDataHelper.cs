using FIOSharp;
using FIOSharp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProsperousAssistant
{
	/// <summary>
	/// This is for managing and caching our FIO data
	/// </summary>
	public class CachedDataHelper : IVariableDataSource
	{
		public FnarOracleDataSource FIO { get; private set; }
		public LocalJsonDataSource JsonData { get; private set; }

		//Time in seconds that it takes for exchange data to be considered "stale" and the cache invalidated. Default is 15 minutes
		public uint ExchangeDataStaleAfter = 54000;

		private IReadOnlyList<Material> materials = null;
		private IReadOnlyDictionary<string, Material> materialsDictionary = null;
		private IReadOnlyList<Building> buildings = null;
		private IReadOnlyList<ExchangeData> exchanges = null;
		//the last time full exchange data was pulled (for staleness checking)
		private DateTime lastExchangeData = DateTime.MinValue;
		private IReadOnlyList<Recipe> recipes = null;
		private IReadOnlyList<WorkforceRequirement> workforceRequirements = null;
		private IReadOnlyDictionary<PopulationType, WorkforceRequirement> workforceRequirementsDictionary = null;

		public List<Material> Materials => GetMaterials();
		public IReadOnlyDictionary<string, Material> MaterialsDictionary
		{
			get {
				if(materialsDictionary == null)
				{
					materialsDictionary = Materials.ToDictionary(mat => mat.Ticker);
				}
				return materialsDictionary;
			}
		}
		public List<Building> Buildings => GetBuildings();
		public List<ExchangeData> Exchanges => GetExchanges();
		public List<Recipe> Recipes => GetRecipes();
		public List<WorkforceRequirement> WorkforceRequirements => GetWorkforceRequirements();

		public IReadOnlyDictionary<PopulationType, WorkforceRequirement> WorkforceRequirementsDictionary
		{
			get
			{
				if(workforceRequirementsDictionary == null)
				{
					workforceRequirementsDictionary = WorkforceRequirements.ToDictionary(workforce => workforce.PopulationType);
				}
				return workforceRequirementsDictionary;
			}
		}


		public CachedDataHelper(FnarOracleDataSource oracle, string workingDirectory)
		{
			
			FIO = oracle;
			JsonData = new LocalJsonDataSource(workingDirectory, oracle);
		}

		public List<Building> GetBuildings(List<Material> allMaterials = null)
		{
			if (buildings == null)
			{
				buildings = JsonData.GetBuildings(Materials);
			}

			return new List<Building>(buildings);
		}

		public async Task<List<Building>> GetBuildingsAsync(List<Material> allMaterials = null)
		{
			if (buildings == null)
			{
				buildings = await JsonData.GetBuildingsAsync(await GetMaterialsAsync());
			}

			return new List<Building>(buildings);
		}

		public List<ExchangeEntry> GetEntriesForExchange(ExchangeData exchange, List<Material> allMaterials = null, bool applyToExchange = true)
		{
			if (!Exchanges.Contains(exchange))
			{
				return FIO.GetEntriesForExchange(exchange, Materials, applyToExchange);
			}

			if (lastExchangeData.AddSeconds(ExchangeDataStaleAfter) > DateTime.Now)
			{
				if (applyToExchange) lastExchangeData = DateTime.Now;
				return FIO.GetEntriesForExchanges(Exchanges, Materials, applyToExchange).Where(entry => entry.Exchange == exchange).ToList();
			}
			return exchange.ExchangeEntries.Values.ToList();
		}

		public async Task<List<ExchangeEntry>> GetEntriesForExchangeAsync(ExchangeData exchange, List<Material> allMaterials = null, bool applyToExchange = true)
		{
			if (!(await GetExchangesAsync()).Contains(exchange))
			{
				return await FIO.GetEntriesForExchangeAsync(exchange, Materials, applyToExchange);
			}

			if (lastExchangeData.AddSeconds(ExchangeDataStaleAfter) > DateTime.Now)
			{
				if (applyToExchange) lastExchangeData = DateTime.Now;
				return (await FIO.GetEntriesForExchangesAsync(Exchanges, Materials, applyToExchange)).Where(entry => entry.Exchange == exchange).ToList();
			}
			return exchange.ExchangeEntries.Values.ToList();
		}

		public List<ExchangeEntry> GetEntriesForExchanges(List<ExchangeData> exchanges = null, List<Material> allMaterials = null, bool applyToExchanges = true)
		{
			if (exchanges == null) exchanges = Exchanges;

			if (!exchanges.All(Exchanges.Contains))
			{
				return FIO.GetEntriesForExchanges(exchanges, Materials, applyToExchanges);
			}

			if(lastExchangeData.AddSeconds(ExchangeDataStaleAfter).CompareTo(DateTime.Now) < 0)
			{
				if(applyToExchanges)lastExchangeData = DateTime.Now;
				return FIO.GetEntriesForExchanges(Exchanges, Materials, applyToExchanges).Where(entry => exchanges.Contains(entry.Exchange)).ToList();
			}

			return exchanges.SelectMany(exchange => exchange.ExchangeEntries.Values).ToList();
		}

		public async Task<List<ExchangeEntry>> GetEntriesForExchangesAsync(List<ExchangeData> exchanges = null, List<Material> allMaterials = null, bool applyToExchanges = true)
		{
			if (exchanges == null) exchanges = Exchanges;

			if (!exchanges.All((await GetExchangesAsync()).Contains))
			{
				return await FIO.GetEntriesForExchangesAsync(exchanges, Materials, applyToExchanges);
			}

			if (lastExchangeData.AddSeconds(ExchangeDataStaleAfter).CompareTo(DateTime.Now) < 0)
			{
				if (applyToExchanges) lastExchangeData = DateTime.Now;
				return (await FIO.GetEntriesForExchangesAsync(Exchanges, await GetMaterialsAsync(), applyToExchanges)).Where(entry => exchanges.Contains(entry.Exchange)).ToList();
			}

			return exchanges.SelectMany(exchange => exchange.ExchangeEntries.Values).ToList();
		}

		public ExchangeEntry GetEntryForExchange(ExchangeData exchange, Material material, bool applyToExchange = true)
		{
			return FIO.GetEntryForExchange(exchange, material, applyToExchange);
		}

		public Task<ExchangeEntry> GetEntryForExchangeAsync(ExchangeData exchange, Material material, bool applyToExchange = true)
		{
			return FIO.GetEntryForExchangeAsync(exchange, material, applyToExchange);
		}

		public List<ExchangeData> GetExchanges()
		{
			if(exchanges == null)
			{
				exchanges = FIO.GetExchanges();
			}
			return new List<ExchangeData>(exchanges);
		}

		public async Task<List<ExchangeData>> GetExchangesAsync()
		{
			if (exchanges == null)
			{
				exchanges = await FIO.GetExchangesAsync();
			}
			return new List<ExchangeData>(exchanges);
		}

		public List<Material> GetMaterials()
		{
			if(materials == null)
			{
				materials = JsonData.GetMaterials();
			}
			return new List<Material>(materials);
		}

		public async Task<List<Material>> GetMaterialsAsync()
		{
			if (materials == null)
			{
				materials = await JsonData.GetMaterialsAsync();
			}
			return new List<Material>(materials);
		}

		public List<Recipe> GetRecipes(List<Material> allMaterials = null, List<Building> buildings = null)
		{
			if(recipes == null)
			{
				recipes = JsonData.GetRecipes(Materials, Buildings);
			}
			return new List<Recipe>(recipes);
		}

		public async Task<List<Recipe>> GetRecipesAsync(List<Material> allMaterials = null, List<Building> buildings = null)
		{
			if (recipes == null)
			{
				recipes = await JsonData.GetRecipesAsync(await GetMaterialsAsync(), await GetBuildingsAsync());
			}
			return new List<Recipe>(recipes);
		}

		public List<WorkforceRequirement> GetWorkforceRequirements(List<Material> allMaterials = null)
		{
			if(workforceRequirements == null)
			{
				workforceRequirements = JsonData.GetWorkforceRequirements(Materials);
			}
			return new List<WorkforceRequirement>(workforceRequirements);
		}

		public async Task<List<WorkforceRequirement>> GetWorkforceRequirementsAsync(List<Material> allMaterials = null)
		{
			if (workforceRequirements == null)
			{
				workforceRequirements = await JsonData.GetWorkforceRequirementsAsync(await GetMaterialsAsync());
			}
			return new List<WorkforceRequirement>(workforceRequirements);
		}
	}
}
