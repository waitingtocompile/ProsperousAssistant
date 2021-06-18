using FIOSharp;
using FIOSharp.Data;
using Newtonsoft.Json.Linq;
using ProsperousAssistant.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProsperousAssistant.ShipmentModel
{
	public class ShipmentCalculator
	{
		public CachedDataHelper DataHelper { get; }

		public Dictionary<Material, ShipmentRow> ImportRows { get; private set; } = new Dictionary<Material, ShipmentRow>();
		public Dictionary<Material, ShipmentRow> ExportRows { get; private set; } = new Dictionary<Material, ShipmentRow>();

		public Dictionary<Material, decimal> ImportAutoFill { get; private set; } = new Dictionary<Material, decimal>();

		public int ImportFF = 0;
		public int ImportSF = 0;
		public int ExportFF = 0;
		public int ExportSF = 0;
		//these are needed for doing autofill and by the UI.
		public int MaxMass = 500;
		public int MaxVolume = 500;


		//Whether to consider Remote exchanges and price differentials, or to use the simpler single exchange calculations and display
		public bool IsArbitrageMode = false;
		//If, when autofilling, existing quantities of materials that are being autofilled should added to (if true) or overriden (if false)
		public bool PreserveQuantitiesOnAutofill = true;
		//whether to look at the remote exchange for fuel costs. Treated as being false when in arbitrage mode
		public bool UseRemoteExchangeForFuelCosts = false;

		public ExchangeData LocalExchange;
		public ExchangeData RemoteExchange;

		public PriceMode ExportPriceMode = PriceMode.AVERAGE;
		public PriceMode ImportPriceMode = PriceMode.AVERAGE;
		public PriceMode FuelPriceMode = PriceMode.AVERAGE;

		public ExchangeData ImportExchange => IsArbitrageMode ? RemoteExchange : LocalExchange;
		public ExchangeData FuelExchange => IsArbitrageMode && UseRemoteExchangeForFuelCosts ? RemoteExchange : LocalExchange;

		public decimal TotalFuelCost => Utils.GetFuelCost(FuelPriceMode, FuelExchange, ImportSF + ExportSF, ImportFF + ExportFF);
		public decimal TotalImportMass => ImportRows.Values.Sum(row => row.TotalMass);
		public decimal TotalImportVolume => ImportRows.Values.Sum(row => row.TotalVolume);
		//import price
		public decimal? TotalImportPrice => ImportRows.Values.Sum(row => row.TotalPrice);
		//import payout
		public decimal? TotalImportInversePrice => ImportRows.Values.Sum(row => row.TotalInversePrice);

		public decimal TotalExportMass => ExportRows.Values.Sum(row => row.TotalMass);
		public decimal TotalExportVolume => ExportRows.Values.Sum(row => row.TotalVolume);
		//export payout
		public decimal? TotalExportPrice => ExportRows.Values.Sum(row => row.TotalPrice);
		//export price
		public decimal? TotalExportInversePrice => ExportRows.Values.Sum(row => row.TotalInversePrice);

		public ShipmentCalculator(CachedDataHelper dataHelper)
		{
			DataHelper = dataHelper;
			LocalExchange = DataHelper.Exchanges.First();
			RemoteExchange = DataHelper.Exchanges.First();
		}


		//this fills out the remaining space in the shipment *roughly* according to autofill amounts
		public void ApplyAutoFillToImport()
		{
			IEnumerable<ShipmentRow> consideredRows = ImportRows.Values;
			if (!PreserveQuantitiesOnAutofill)
			{
				//remove existing stuff from our considered rows
				consideredRows = consideredRows.Where(row => !ImportAutoFill.ContainsKey(row.Material));
			}

			decimal massLeft = consideredRows.Sum(row => row.TotalMass);
			decimal volumeLeft = consideredRows.Sum(row => row.TotalVolume);

			decimal unitMass = ImportAutoFill.Sum(pair => pair.Key.Mass * pair.Value);
			decimal unitVolume = ImportAutoFill.Sum(pair => pair.Key.Volume * pair.Value);

			decimal scaleFactor = Math.Min(massLeft / unitMass, volumeLeft / unitVolume);
			var amounts = ImportAutoFill.Select(pair => KeyValuePair.Create(pair.Key, (int)Math.Round(pair.Value * scaleFactor) - 1));
			foreach(var pair in amounts)
			{
				if (PreserveQuantitiesOnAutofill)
				{
					CreateOrGrowRow(true, pair.Key, pair.Value);
				}
				else
				{
					CreateOrReplaceRow(true, pair.Key, pair.Value);
				}
			}



		}

		//Create a row, or replace the existing one if it already exists
		public ShipmentRow CreateOrReplaceRow(bool isImport, Material mat, int amount)
		{
			Dictionary<Material, ShipmentRow> dict = isImport ? ImportRows : ExportRows;
			if (dict.ContainsKey(mat))
			{
				//we already have a row, ovewrite it's value
				ShipmentRow row = dict[mat];
				row.Amount = amount;
				return row;
			}
			else
			{
				ShipmentRow row = new ShipmentRow(this, mat, amount, isImport);
				dict.Add(mat, row);
				return row;
			}
		}

		//Create a row, or grow the existing one by the given amount if it exists already
		public ShipmentRow CreateOrGrowRow(bool isImport, Material mat, int amount)
		{
			Dictionary<Material, ShipmentRow> dict = isImport ? ImportRows : ExportRows;
			if (dict.ContainsKey(mat))
			{
				//we already have a row, adjust it's value
				ShipmentRow row = dict[mat];
				row.Amount += amount;
				return row;
			}
			else
			{
				ShipmentRow row = new ShipmentRow(this, mat, amount, isImport);
				dict.Add(mat, row);
				return row;
			}
		}


		public JObject RowsToJson()
		{
			JObject jObject = new JObject();
			JObject importObject = new JObject();
			foreach(var row in ImportRows.Values)
			{
				importObject.Add(row.MaterialTicker, row.Amount);
			}
			jObject.Add("ImportRows", importObject);

			JObject exportObject = new JObject();
			foreach (var row in ExportRows.Values)
			{
				exportObject.Add(row.MaterialTicker, row.Amount);
			}
			jObject.Add("ExportRows", exportObject);

			

			return jObject;
		}

		public void RowsFromJson(JObject jObject)
		{
			try
			{
				ImportRows.Clear();
				JObject importObject = (JObject)jObject.GetValue("ImportRows");
				foreach(var property in importObject.Properties())
				{
					CreateOrReplaceRow(true, DataHelper.MaterialsDictionary[property.Name], property.Value.ToObject<int>());
				}


				ExportRows.Clear();
				JObject exportObject = (JObject)jObject.GetValue("ExportRows");
				foreach (var property in exportObject.Properties())
				{
					CreateOrReplaceRow(true, DataHelper.MaterialsDictionary[property.Name], property.Value.ToObject<int>());
				}
			}
			catch (Exception ex)
			{
				throw new JsonSchemaException("Improper format in stored shipment calculator.", ex);
			}
			
		}

		public JObject SettingsToJson()
		{
			JObject jObject = new JObject();

			JObject autofillObject = new JObject();
			foreach (var pair in ImportAutoFill)
			{
				autofillObject.Add(pair.Key.Ticker, pair.Value);
			}
			jObject.Add("Autofill", autofillObject);

			jObject.Add("ImportFF", ImportFF);
			jObject.Add("ImportSF", ImportSF);
			jObject.Add("ExportFF", ExportFF);
			jObject.Add("ExportSF", ExportSF);
			jObject.Add("Mass", MaxMass);
			jObject.Add("Volume", MaxVolume);

			jObject.Add("ArbitrageMode", IsArbitrageMode);
			jObject.Add("PreserveAutofill", PreserveQuantitiesOnAutofill);
			jObject.Add("RemoteFuel", UseRemoteExchangeForFuelCosts);
			jObject.Add("LocalExchange", LocalExchange.Ticker);
			jObject.Add("RemoteExchange", RemoteExchange.Ticker);
			jObject.Add("ExportMode", ExportPriceMode.Name);
			jObject.Add("ImportPriceMode", ImportPriceMode.Name);
			jObject.Add("FuelPriceMode", FuelPriceMode.Name);

			return jObject;
		}

		public void SettingsFromJson(JObject jObject)
		{
			try
			{
				ImportAutoFill.Clear();
				JObject autofillObject = (JObject)jObject.GetValue("Autofill");
				foreach (var property in autofillObject.Properties())
				{
					ImportAutoFill.Add(DataHelper.MaterialsDictionary[property.Name], property.Value.ToObject<decimal>());
				}

				ImportFF = jObject.GetValue("ImportFF").ToObject<int>();
				ImportSF = jObject.GetValue("ImportSF").ToObject<int>();
				ExportFF = jObject.GetValue("ExportFF").ToObject<int>();
				ExportSF = jObject.GetValue("ExportSF").ToObject<int>();
				MaxMass = jObject.GetValue("Mass").ToObject<int>();
				MaxVolume = jObject.GetValue("Volume").ToObject<int>();
				IsArbitrageMode = jObject.GetValue("ArbitrageMode").ToObject<bool>();
				PreserveQuantitiesOnAutofill = jObject.GetValue("PreserveAutofill").ToObject<bool>();
				UseRemoteExchangeForFuelCosts = jObject.GetValue("RemoteFuel").ToObject<bool>();
				LocalExchange = Utils.GetExchangeByTicker(DataHelper, jObject.GetValue("LocalExchange").ToObject<string>());
				RemoteExchange = Utils.GetExchangeByTicker(DataHelper, jObject.GetValue("RemoteExchange").ToObject<string>());
				ExportPriceMode = jObject.GetValue("ExportMode").ToObject<PriceMode>();
				ImportPriceMode = jObject.GetValue("ImportMode").ToObject<PriceMode>();
				FuelPriceMode = jObject.GetValue("FuelMode").ToObject<PriceMode>();

			}
			catch (Exception ex)
			{
				throw new JsonSchemaException("Improper format in shipment calculator settings.", ex);
			}
		}
	}
}
