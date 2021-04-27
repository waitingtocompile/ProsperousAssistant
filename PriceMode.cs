using FIOSharp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProsperousAssistant
{
	public class PriceMode
	{
		public Func<ExchangeEntry, decimal?> Func { get; }
		public string Name { get;}

		public static readonly Dictionary<string, PriceMode> ALL_MODES = new Dictionary<string, PriceMode>(StringComparer.OrdinalIgnoreCase);

		public static readonly PriceMode ASK = new PriceMode(entry => entry.Ask, "Ask");
		public static readonly PriceMode BID = new PriceMode(entry => entry.Bid, "Bid");
		public static readonly PriceMode AVERAGE = new PriceMode(entry => entry.PriceAverage, "Average");
		public static readonly PriceMode MM_BUY = new PriceMode(entry => entry.MMBuy, "MM Buy");
		public static readonly PriceMode MM_SELL = new PriceMode(entry => entry.MMSell, "MM Sell");

		private PriceMode(Func<ExchangeEntry, decimal?> func, string name)
		{
			Func = func;
			Name = name;
			ALL_MODES.Add(name, this);
		}

		//for backwards compatabillity with the old system
		public static PriceMode Parse(string str)
		{
			return ALL_MODES[str];
		}

		public decimal? Get(ExchangeData exchangeData, Material mat)
		{
			return Func(exchangeData.ExchangeEntries[mat]);
		}

		public decimal? Get(IEnumerable<ExchangeEntry> exchangeEntries, Material mat)
		{
			return Func(exchangeEntries.Where(entry => entry.Material == mat).Single());
		}

		public decimal? Get(ExchangeEntry exchangeEntry)
		{
			return Func(exchangeEntry);
		}
	}

	public class PriceModeConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			PriceMode mode = (PriceMode)value;

			writer.WriteValue(mode.Name);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			
			string name = (string)reader.Value;
			return PriceMode.Parse(name);
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(PriceMode);
		}
	}
}
