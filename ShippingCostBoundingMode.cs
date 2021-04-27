using FIOSharp.Data;
using System;
using System.Collections.Generic;

namespace ProsperousAssistant
{
	public class ShippingCostBoundingMode
	{
		public static IReadOnlyDictionary<string, ShippingCostBoundingMode> ALL_MODES => allModes;
		private static Dictionary<string, ShippingCostBoundingMode> allModes = new Dictionary<string, ShippingCostBoundingMode>(StringComparer.OrdinalIgnoreCase);




		public static readonly ShippingCostBoundingMode MASS = new ShippingCostBoundingMode("Mass",
			(decimal mass, decimal volume, decimal maxMass, decimal maxVolume) => mass/maxMass);
		public static readonly ShippingCostBoundingMode VOLUME = new ShippingCostBoundingMode("Volume",
			(decimal mass, decimal volume, decimal maxMass, decimal maxVolume) => volume / maxVolume);
		public static readonly ShippingCostBoundingMode MAX = new ShippingCostBoundingMode("Max",
			(decimal mass, decimal volume, decimal maxMass, decimal maxVolume) => Math.Max(volume / maxVolume, mass/maxMass));
		public static readonly ShippingCostBoundingMode MIN = new ShippingCostBoundingMode("Min",
			(decimal mass, decimal volume, decimal maxMass, decimal maxVolume) => Math.Min(volume / maxVolume, mass / maxMass));
		public static readonly ShippingCostBoundingMode AVERAGE = new ShippingCostBoundingMode("Average",
			(decimal mass, decimal volume, decimal maxMass, decimal maxVolume) => (volume / maxVolume + mass / maxMass)/2);


		private ShippingCostBoundingMode(string name, Func<decimal, decimal, decimal, decimal, decimal> func)
		{
			Name = name;
			this.func = func;
			allModes.Add(Name, this);
		}

		public string Name { get; }
		private Func<decimal, decimal, decimal, decimal, decimal> func;

		public decimal GetLoadPercentage(Material material, decimal maxMass, decimal maxVolume)
		{
			return GetLoadPercentage(material.Mass, material.Volume, maxMass, maxVolume);
		}

		public decimal GetLoadPercentage(decimal mass, decimal volume, decimal maxMass, decimal maxVolume)
		{
			return func(mass, volume, maxMass, maxVolume);
		}

	}
}
