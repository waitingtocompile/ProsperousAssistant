using FIOSharp.Data;
using System.Linq;

namespace ProsperousAssistant.ProductionModel
{

	/// <summary>
	/// This is the object equivelant of a line in the "production selection" table
	/// </summary>
	public class ProfitEstimator
	{
		public ProfitEstimatorSettings Settings { get; private set; }

		public Recipe Recipe { get; private set; }
		public Building Building => Recipe.Building;
		public string BuildingTicker => Building.Ticker;
		public string OutputIdentifier { get
			{
				if(_outputIdentifier == null)
				{
					_outputIdentifier = string.Join(", ", Recipe.Outputs.Keys.Select(mat => mat.Ticker));
				}
				return _outputIdentifier;
			} }
		private string _outputIdentifier;

		//todo: look for an elegant solution to display the input/output amounts

		public string VariantIndicator => VariantFinder.GetVariantString(Recipe);

		//todo: consider value caching to save execution time?

		//Base production time in our production selector's chosen units
		public decimal BaseProductionTime => (decimal)Recipe.Duration / Settings.ProductionTimeScale.ScaleFactor;

		public decimal SpeedModifier => Settings.GetBuildingSpeedMod(Building);

		public decimal? AdjustedProductionTime
		{
			get
			{
				if (SpeedModifier > 0) return BaseProductionTime / SpeedModifier;
				return null;
			}
		}

		public decimal? MaterialCost => Recipe.Inputs.Select(pair => Settings.GetPurchasePrice(pair.Key) * pair.Value).Aggregate((decimal?)0, (val1, val2) => val1 + val2);

		public decimal? SalePrice => Recipe.Outputs.Select(pair => Settings.GetSalePrice(pair.Key) * pair.Value).Aggregate((val1, val2) => val1 + val2);

		public decimal? ImportCost => Recipe.Inputs.Select(pair => Settings.GetImportCost(pair.Key) * pair.Value).Aggregate((decimal?)0, (val1, val2) => val1 + val2);

		public decimal? ExportCost => Recipe.Outputs.Select(pair => Settings.GetExportCost(pair.Key) * pair.Value).Aggregate((val1, val2) => val1 + val2);

		public decimal? ConsumableCost => Settings.GetScaledConsumableCost(Recipe.Building) * AdjustedProductionTime;

		public decimal? ProductionFee => Settings.GetScaledProductionFee(Recipe.Building) * BaseProductionTime;

		public decimal? CycleCost => MaterialCost + ImportCost + ExportCost + ProductionFee + ConsumableCost;

		public decimal? CycleProfit => SalePrice - CycleCost;

		public decimal? TimescaleProfit => (AdjustedProductionTime>0)?CycleProfit / AdjustedProductionTime:0;

		public decimal? PercentageProfit => CycleProfit / CycleCost;

		public ProfitEstimator(ProfitEstimatorSettings settings, Recipe recipe)
		{
			Settings = settings;
			Recipe = recipe;
		}

	}
}
