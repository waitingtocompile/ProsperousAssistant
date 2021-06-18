using FIOSharp.Data;
using Newtonsoft.Json.Linq;
using ProsperousAssistant.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProsperousAssistant.ShipmentModel
{
	public class ShipmentRow
	{
		

		public ShipmentCalculator Calculator { get; }
		public Material Material { get; }
		public string MaterialTicker => Material.Ticker;
		public bool IsImportRow { get; }
		public int Amount { get; set; }

		public ShipmentRow(ShipmentCalculator calculator, Material mat, int amount, bool isImportRow)
		{
			Calculator = calculator;
			Material = Material;
			IsImportRow = isImportRow;
			Amount = amount;
		}

		public PriceMode PriceMode => IsImportRow ? Calculator.ImportPriceMode : Calculator.ExportPriceMode;
		public PriceMode InversePriceMode => IsImportRow ? Calculator.ExportPriceMode : Calculator.ImportPriceMode;
		public ExchangeData Exchange => IsImportRow ? Calculator.ImportExchange : Calculator.LocalExchange;
		public ExchangeData InverseExchange => IsImportRow ? Calculator.LocalExchange : Calculator.ImportExchange;

		public decimal? Price => PriceMode.Get(Exchange, Material);
		public decimal? InversePrice => InversePriceMode.Get(InverseExchange, Material);

		public decimal? TotalPrice => Price * Amount;

		//The price at the other exchange and price mode (for comparison in arbitrage mode)
		public decimal? TotalInversePrice => InversePrice * Amount;

		public decimal TotalMass => Material.Mass * Amount;
		public decimal TotalVolume => Material.Volume * Amount;
	}
}
