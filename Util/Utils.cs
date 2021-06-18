using FIOSharp;
using FIOSharp.Data;
using System;
using System.Linq;

namespace ProsperousAssistant.Util
{
	//an assortment of utility functions that don't really belong anywhere but are widely useful
	public static class Utils
	{
		public static decimal GetFuelCost(PriceMode priceMode, ExchangeData exchange, int SF, int FF)
		{
			return priceMode.Get(GetEntryByTicker(exchange, "SF")).Value * SF + priceMode.Get(GetEntryByTicker(exchange, "FF")).Value * FF;
		}

		public static ExchangeEntry GetEntryByTicker(ExchangeData exchange, string ticker)
		{
			Material foundMaterial;
			try
			{
				foundMaterial = exchange.ExchangeEntries.Keys.Where(mat => mat.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase)).Single();
			}
			catch
			{
				throw new ArgumentException($"Unknown material ticker [{ticker}]");
			}
			return exchange.ExchangeEntries[foundMaterial];
		}

		public static ExchangeData GetExchangeByTicker(IVariableDataSource dataSource, string ticker)
		{
			return dataSource.GetExchanges().Where(exchange => exchange.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase)).Single();
		}
	}
}
