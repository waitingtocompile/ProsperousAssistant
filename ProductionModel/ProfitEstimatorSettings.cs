using FIOSharp;
using FIOSharp.Data;
using Newtonsoft.Json.Linq;
using ProsperousAssistant.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProsperousAssistant.ProductionModel
{
	/// <summary>
	/// Configuration information for a group of ProductionSelections
	/// </summary>
	public class ProfitEstimatorSettings
	{

		public static readonly decimal[] ExpertLevels = { 0m, 0.0306m, 0.0696m, 0.1248m, 0.1975m, 0.2840m };

		//Used to configure the chosen time scale mode
		public TimeScale ProductionTimeScale = TimeScale.HOURS;

		public PriceMode BuyPriceMode = PriceMode.AVERAGE;
		public PriceMode SellPriceMode = PriceMode.AVERAGE;
		public PriceMode ConsumablesPriceMode = PriceMode.AVERAGE;
		public PriceMode FuelPriceMode = PriceMode.AVERAGE;
		//todo: building maintainance price mode

		public IReadOnlyDictionary<PopulationType, PopulationOptions> PopulationSettings;

		private readonly Dictionary<string, int> expertAllocation = Building.EXPERTISE_ALL.ToDictionary(s => s, s => 0);
		private readonly Dictionary<string, decimal> productionFees = Building.EXPERTISE_ALL.ToDictionary(s => s, s => 10m);


		public bool IgnoreImportCost = false;
		public bool IgnoreExportCost = false;
		public ShippingCostBoundingMode ShippingCostMode = ShippingCostBoundingMode.MAX;
		public int ShipMassCapacity = 500;
		public int ShipVolumeCapacity = 500;
		public decimal ShipFillMargin = 0;//the amount of the ship's hold that should be discarded before calculating fill levels
		public int ImportSF = 0;
		public int ImportFF = 0;
		public int ExportSF = 0;
		public int ExportFF = 0;
		public readonly HashSet<Material> IgnoreShipping = new HashSet<Material>();


		//todo: account for buying different mats from different exchanges?
		public ExchangeData Exchange;

		//todo: building condition options


		//todo: consider caching material and building data?

		public CachedDataHelper DataHelper { get; }


		public ProfitEstimatorSettings(CachedDataHelper dataHelper)
		{
			DataHelper = dataHelper;
			PopulationSettings = PopulationOptions.CreateAll(dataHelper.WorkforceRequirements, true, false).ToDictionary(option => option.PopulationType, option => option);
			Exchange = DataHelper.Exchanges.First();
			//to make sure our dataHelper is initially populated
			DataHelper.GetEntriesForExchanges();
		}

		public int GetExpertsForExpertise(string expertise)
		{
			if (expertAllocation.ContainsKey(expertise)) return expertAllocation[expertise];
			throw new ArgumentException("Unknown expertise type " + expertise);
		}

		/// <summary>
		/// Set the experts for a given expertise. Won't go over the limit (5) but DOES NOT check the total active expert limit
		/// </summary>
		public void SetExpertsForExpertise(string expertise, int experts)
		{
			if (expertAllocation.ContainsKey(expertise)) expertAllocation[expertise] = Math.Max(Math.Min(experts, 5), 0);
			else throw new ArgumentException("Unknown expertise type " + expertise);
		}

		//this is the *per day* production fee
		public decimal GetBaseProductionFee(string expertise) {
			if(productionFees.ContainsKey(expertise))return productionFees[expertise];
			throw new ArgumentException("Unknown expertise type " + expertise);
		}

		//this is the *per day* production fee
		public void SetBaseProductionFee(string expertise, decimal fee)
		{
			if (productionFees.ContainsKey(expertise)) productionFees[expertise] = Math.Max(0, fee);
			else throw new ArgumentException("Unknown expertise type " + expertise);
		}

		public decimal GetBuildingSpeedMod(Building building)
		{
			decimal modifier = 0;
			int totalPopulation = building.Populations.Values.Sum();
			foreach ((PopulationType population, int count) in building.Populations)
			{
				decimal share = count / (decimal)totalPopulation;
				PopulationOptions options = PopulationSettings[population];
				modifier += options.SpeedFactor * share;
			}
			if(expertAllocation.ContainsKey(building.Expertise))modifier *= 1 + ExpertLevels[expertAllocation[building.Expertise]];

			//todo: production line condition, COGC bonuses, HQ bonuses
			return modifier;
		}


		public decimal? GetImportCost(Material material)
		{
			if (IgnoreImportCost || IgnoreShipping.Contains(material)) return 0;
			
			decimal percentUsed = ShippingCostMode.GetLoadPercentage(material, ShipMassCapacity * (1 - ShipFillMargin), ShipVolumeCapacity * (1 - ShipFillMargin));
			return percentUsed * (ImportFF * FuelPriceMode.Get(Exchange, DataHelper.MaterialsDictionary["FF"]).Value + ImportSF * FuelPriceMode.Get(Exchange, DataHelper.MaterialsDictionary["SF"]));
		}

		public decimal? GetExportCost(Material material)
		{
			if (IgnoreExportCost || IgnoreShipping.Contains(material)) return 0;

			decimal percentUsed = ShippingCostMode.GetLoadPercentage(material, ShipMassCapacity * (1 - ShipFillMargin), ShipVolumeCapacity * (1 - ShipFillMargin));
			decimal? cost =  percentUsed * (ExportFF * FuelPriceMode.Get(Exchange, DataHelper.MaterialsDictionary["FF"]) + ExportSF * FuelPriceMode.Get(Exchange, DataHelper.MaterialsDictionary["SF"]));
			return cost;
		}
		
		public decimal? GetPurchasePrice(Material material)
		{
			return BuyPriceMode.Get(Exchange, material);
		}

		public decimal? GetSalePrice(Material material)
		{
			return SellPriceMode.Get(Exchange, material);
		}

		public decimal? GetConsumablePrice(Material material)
		{
			return ConsumablesPriceMode.Get(Exchange, material);
		}

		//Gets the production fee of a building per timescale interval, before building speed modifiers
		public decimal GetScaledProductionFee(Building building)
		{
			return GetBaseProductionFee(building.Expertise) / (TimeScale.DAYS.ScaleFactor / ProductionTimeScale.ScaleFactor);
		}

		///Get the amount of consumables used by a given building uses per timescale interval
		public Dictionary<Material, decimal> GetScaledConsumableConsumption(Building building)
		{
			//consider caching?
			IReadOnlyDictionary<PopulationType, WorkforceRequirement> workforceRequirements = DataHelper.WorkforceRequirementsDictionary;
			Dictionary<Material, decimal> consumptions = new Dictionary<Material, decimal>();
			IEnumerable<KeyValuePair<PopulationType, int>> activePops = building.Populations.Where(pair => PopulationSettings[pair.Key].Active);
			
			foreach(var pair in activePops)
			{
				foreach(var materialPair in workforceRequirements[pair.Key].Requirements.Where(requirementPair => PopulationSettings[pair.Key].GetEnabled(requirementPair.Key)))
				{
					decimal totalNeeded = materialPair.Value.count * (ProductionTimeScale.ScaleFactor / (decimal)TimeScale.DAYS.ScaleFactor) / 100 * pair.Value;
					if(!consumptions.ContainsKey(materialPair.Key))
					{
						consumptions[materialPair.Key] = totalNeeded;
					}
					else
					{
						consumptions[materialPair.Key] += totalNeeded;
					}
				}
			}

			return consumptions;
		}
		
		//gets the cost of consumables used by a given building 
		public decimal? GetScaledConsumableCost(Building building)
		{
			return GetScaledConsumableConsumption(building).Select(pair => GetConsumablePrice(pair.Key) * pair.Value).Aggregate((first, second) => first + second);
		}

		public JObject ToJson()
		{
			JObject jObject = new JObject();
			jObject.Add("TimeScale", ProductionTimeScale.Name);
			JObject priceModes = new JObject();
			priceModes.Add("Buy", BuyPriceMode.Name);
			priceModes.Add("Sell", SellPriceMode.Name);
			priceModes.Add("Consumables", ConsumablesPriceMode.Name);
			priceModes.Add("Fuel", FuelPriceMode.Name);
			jObject.Add("PriceModes", priceModes);
			JObject populationsObject = new JObject();
			foreach(PopulationOptions population in PopulationSettings.Values)
			{
				populationsObject.Add(population.PopulationType.Name, population.ToJson());
			}
			jObject.Add("Populations", populationsObject);
			JObject expertsObject = new JObject();
			foreach(var alloc in expertAllocation)
			{
				expertsObject.Add(alloc.Key, alloc.Value);
			}
			jObject.Add("Experts", expertsObject);
			jObject.Add("IgnoreImportCost", IgnoreImportCost);
			jObject.Add("IgnoreExportCost", IgnoreExportCost);
			jObject.Add("ShippingCostMode", ShippingCostMode.Name);
			jObject.Add("MaxMass", ShipMassCapacity);
			jObject.Add("MaxVolume", ShipVolumeCapacity);
			jObject.Add("FillMargin", ShipFillMargin);
			jObject.Add("ImportSF", ImportSF);
			jObject.Add("ImportFF", ImportFF);
			jObject.Add("ExportSF", ExportSF);
			jObject.Add("ExportFF", ExportFF);
			JArray noShipArray = new JArray();
			foreach(Material mat in IgnoreShipping)
			{
				noShipArray.Add(mat.Ticker);
			}
			jObject.Add("NoShip", noShipArray);
			jObject.Add("Exchange", Exchange.Ticker);


			return jObject;
		}

		//read settings from a jObject and apply
		public void FromJson(JObject jObject)
		{

			try
			{
				//read everything
				ProductionTimeScale = TimeScale.Parse(jObject.GetValue("TimeScale").ToObject<string>());
				JObject priceModesObject = (JObject)jObject.GetValue("PriceModes");
				BuyPriceMode = PriceMode.Parse(priceModesObject.GetValue("Buy").ToObject<string>());
				SellPriceMode = PriceMode.Parse(priceModesObject.GetValue("Sell").ToObject<string>());
				ConsumablesPriceMode = PriceMode.Parse(priceModesObject.GetValue("Consumables").ToObject<string>());
				FuelPriceMode = PriceMode.Parse(priceModesObject.GetValue("Fuel").ToObject<string>());
				JObject populationsObject = (JObject)jObject.GetValue("Populations");
				foreach(PopulationOptions population in PopulationSettings.Values)
				{
					population.FromJson((JObject)populationsObject.GetValue(population.PopulationType.Name));
				}
				JObject expertsObject = (JObject)jObject.GetValue("Experts");
				foreach(string expertType in expertAllocation.Keys.ToList())
				{
					SetExpertsForExpertise(expertType, expertsObject.GetValue(expertType).ToObject<int>());
				}
				IgnoreImportCost = jObject.GetValue("IgnoreImportCost").ToObject<bool>();
				IgnoreExportCost = jObject.GetValue("IgnoreExportCost").ToObject<bool>();
				ShippingCostMode = ShippingCostBoundingMode.ALL_MODES[jObject.GetValue("ShippingCostMode").ToObject<string>()];
				ShipMassCapacity = jObject.GetValue("MaxMass").ToObject<int>();
				ShipVolumeCapacity = jObject.GetValue("MaxVolume").ToObject<int>();
				ShipFillMargin = jObject.GetValue("FillMargin").ToObject <decimal>();
				ImportSF = jObject.GetValue("ImportSF").ToObject<int>();
				ImportFF = jObject.GetValue("ImportFF").ToObject<int>();
				ExportSF = jObject.GetValue("ExportSF").ToObject<int>();
				ExportFF = jObject.GetValue("ExportFF").ToObject<int>();
				JArray noShipArray = (JArray)jObject.GetValue("NoShip");
				if (noShipArray != null)
				{
					IgnoreShipping.Clear();
					Dictionary<string, Material> materials = DataHelper.Materials.ToDictionary(mat => mat.Ticker, mat => mat);
					foreach(JToken token in noShipArray)
					{
						IgnoreShipping.Add(materials[token.ToObject<string>()]);
					}
				}
				Exchange = DataHelper.Exchanges.Where(exchange => exchange.Ticker.Equals(jObject.GetValue("Exchange").ToObject<string>())).Single();

			}
			catch(Exception ex)
			{
				//something went wrong
				throw new JsonSchemaException("Improper format in profit estimator settings preset.", ex);
			}
		}
	}
}
