using FIOSharp;
using FIOSharp.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProsperousAssistant.ProductionModel
{
	public class PopulationOptions
	{
		private static Dictionary<string, decimal> KnownEfficiencyFactors;
		private static Dictionary<PopulationType, decimal> BaseOperatingSpeed;

		static PopulationOptions()
		{
			KnownEfficiencyFactors = new Dictionary<string, decimal>();
			KnownEfficiencyFactors.Add("DW", 10m / 3m);
			KnownEfficiencyFactors.Add("RAT", 4m);
			KnownEfficiencyFactors.Add("OVE", 5m / 6m);
			KnownEfficiencyFactors.Add("PWO", 1m/11m);
			KnownEfficiencyFactors.Add("COF", 2m / 13m);
			KnownEfficiencyFactors.Add("EXO", 1m);
			KnownEfficiencyFactors.Add("PT", 5m/6m);

			//these ones are suspected but unconfirmed
			KnownEfficiencyFactors.Add("KOM", 2m / 13m);
			KnownEfficiencyFactors.Add("REP", 1m / 11m);

			BaseOperatingSpeed = new Dictionary<PopulationType, decimal>();
			BaseOperatingSpeed.Add(PopulationType.PIONEER, 0.02m);
			BaseOperatingSpeed.Add(PopulationType.SETTLER, 0.01m);
		}


		private bool _active = true;
		public bool Active { get => _active; set { _speedFactor = null; _active = value; } }
		public readonly PopulationType PopulationType;
		private readonly Dictionary<Material, bool> enabledConsumables;
		private decimal? _speedFactor = null;
		public decimal SpeedFactor
		{
			get
			{
				if (!_speedFactor.HasValue)
				{
					if (!Active)
					{
						_speedFactor = 0m;
					}
					else if (!BaseOperatingSpeed.ContainsKey(PopulationType))
					{
						//we don't have data to actually calculate from, just spit out the default
						_speedFactor = 1m;
					}
					else
					{
						_speedFactor = BaseOperatingSpeed[PopulationType];
						foreach(KeyValuePair<Material, bool> pair in enabledConsumables)
						{
							string mat = pair.Key.Ticker.ToUpperInvariant();
							if (pair.Value && KnownEfficiencyFactors.ContainsKey(mat))
							{
								_speedFactor *= 1 + KnownEfficiencyFactors[mat];
							}
						}
					}
				}

				return _speedFactor.Value;
			}
		}

		public PopulationOptions(WorkforceRequirement workforce, bool basicDefault = true, bool luxuryDefault = false)
		{
			PopulationType = workforce.PopulationType;
			enabledConsumables = workforce.Requirements.ToDictionary(pair => pair.Key, pair => pair.Value.isRequired?basicDefault:luxuryDefault);
		}

		public PopulationOptions(PopulationOptions source)
		{
			_active = source._active;
			PopulationType = source.PopulationType;
			enabledConsumables = new Dictionary<Material, bool>(source.enabledConsumables);
		}


		public bool GetEnabled(Material material)
		{
			if (enabledConsumables.ContainsKey(material)) return enabledConsumables[material];
			throw new ArgumentException($"Invalid material {material.Ticker}");
		}

		public void SetEnabled(Material material, bool value)
		{
			_speedFactor = null;
			if (enabledConsumables.ContainsKey(material)) enabledConsumables[material] = value;
			else throw new ArgumentException($"Invalid material {material.Ticker}");
		}

		//provides a list of relevant materials
		public Material[] GetRelevantMaterials()
		{
			return enabledConsumables.Keys.ToArray();
		}

		public static List<PopulationOptions> CreateAll(List<WorkforceRequirement> workforces, bool basicDefault = true, bool luxuryDefault = false)
		{
			return workforces.Select(workforce => new PopulationOptions(workforce, basicDefault, luxuryDefault)).ToList();
		}

		public static List<PopulationOptions> CreateAll(IFixedDataSource fixedDataSource, bool basicDefault = true, bool luxuryDefault = false)
		{
			return CreateAll(fixedDataSource.GetWorkforceRequirements(), basicDefault, luxuryDefault);
		}


		public JObject ToJson()
		{
			JObject jObject = new JObject();
			jObject.Add("IsActive", Active);
			JObject consumablesObject = new JObject();
			foreach(var pair in enabledConsumables)
			{
				consumablesObject.Add(pair.Key.Ticker, pair.Value);
			}
			jObject.Add("Consumables", consumablesObject);

			return jObject;
		}

		public void FromJson(JObject jObject)
		{
			Active = jObject.GetValue("IsActive").ToObject<bool>();
			JObject consumablesObject = (JObject)jObject.GetValue("Consumables");
			foreach(Material mat in enabledConsumables.Keys)
			{
				SetEnabled(mat, consumablesObject.GetValue(mat.Ticker).ToObject<bool>());
			}
		}
	}
}
