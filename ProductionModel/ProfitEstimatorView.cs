using FIOSharp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProsperousAssistant.SettingsForms;
using ProsperousAssistant.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProsperousAssistant.ProductionModel
{
	public partial class ProfitEstimatorView : UserControl, ISavesState
	{
		private struct PopulationView
		{
			public readonly CheckBox Enabled;
			public readonly CheckedListBox Consumables;
			public readonly IReadOnlyDictionary<int, Material> IndexMap;

			public PopulationView(CheckBox enabled, CheckedListBox consumables, IReadOnlyDictionary<int, Material> indexMap)
			{
				Enabled = enabled;
				Consumables = consumables;
				IndexMap = indexMap;
			}
		}

		public string CalculatorPresetsDir => ProsperousAssistant.Settings.StoragePath + "\\profit estimator presets";

		private readonly int sidePanelWidth;
		private int expandedSplitterDistance => MainContainer.Width - sidePanelWidth;
		private int collpasedSplitterDistance => MainContainer.Width;

		private ProfitEstimatorSettings CalcSettings;
		private Dictionary<string, NumericUpDown> ExpertSelectors = new Dictionary<string, NumericUpDown>();
		private Dictionary<string, NumericUpDown> FeeSelectors = new Dictionary<string, NumericUpDown>();
		private Dictionary<PopulationType, PopulationView> PopulationControls = new Dictionary<PopulationType, PopulationView>();
		private bool firstReadDone = false;

		private BindingList<ProfitEstimator> profitEstimators;

		private EditCommodityListForm editCommodityListForm;


		public ProfitEstimatorView(CachedDataHelper dataHelper)
		{
			InitializeComponent();
			Directory.CreateDirectory(CalculatorPresetsDir);

			CalcSettings = new ProfitEstimatorSettings(dataHelper);
			
			DoubleBuffered = true;

			sidePanelWidth = MainContainer.Width - MainContainer.SplitterDistance;

			LinkSettingsComponentsAndRead();
			SetupDataGrid();
		}


		private void LinkSettingsComponentsAndRead()
		{
			

			BindComboBox(TimeScaleSelector, TimeScale.ALL, "Name");
			BindComboBox(ExchangeSelector, CalcSettings.DataHelper.Exchanges.ToArray(), "Ticker");
			BindComboBox(BuyModeSelector, PriceMode.ALL_MODES.Values, "Name");
			BindComboBox(SellModeSelector, PriceMode.ALL_MODES.Values, "Name");
			BindComboBox(ConsumablesModeSelector, PriceMode.ALL_MODES.Values, "Name");
			BindComboBox(FuelModeSelector, PriceMode.ALL_MODES.Values, "Name");
			foreach(string expertise in Building.EXPERTISE_ALL)
			{
				string prettyExpertise = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(expertise.ToLower().Replace('_', ' ')).Replace("Resource", "Res.");



				ExpertiseAndFeesTable.RowCount += 1;
				ExpertiseAndFeesTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				ExpertiseAndFeesTable.Controls.Add(new Label() { Text = prettyExpertise }, 0, ExpertiseAndFeesTable.RowCount - 1);

				NumericUpDown feeCounter = new NumericUpDown();
				feeCounter.DecimalPlaces = 2;
				feeCounter.Maximum = decimal.MaxValue;
				feeCounter.Size = new Size(60, feeCounter.Size.Height);
				feeCounter.ValueChanged += (object sender, EventArgs e) =>
				{
					CalcSettings.SetBaseProductionFee(expertise, feeCounter.Value);
					RefreshGrid();
				};
				FeeSelectors.Add(expertise, feeCounter);
				ExpertiseAndFeesTable.Controls.Add(feeCounter, 1, ExpertiseAndFeesTable.RowCount - 1);

				NumericUpDown expertCounter = new NumericUpDown();
				expertCounter.Maximum = 5;
				expertCounter.Size = new Size(55, expertCounter.Size.Height);
				expertCounter.ValueChanged += (object sender, EventArgs e) =>
				{
					CalcSettings.SetExpertsForExpertise(expertise, (int)expertCounter.Value);
					RefreshGrid();
				};
				ExpertSelectors.Add(expertise, expertCounter);
				ExpertiseAndFeesTable.Controls.Add(expertCounter, 2, ExpertiseAndFeesTable.RowCount - 1);

				
			}

			foreach(PopulationOptions populationOptions in CalcSettings.PopulationSettings.Values)
			{
				CreateAndAddPopulationView(populationOptions);
			}

			BindComboBox(ShipBoundingSelector, ShippingCostBoundingMode.ALL_MODES.Values, "Name");

			if (!ReadPresetFile($"{CalculatorPresetsDir}\\{ProsperousAssistant.Settings.LastSettingPath}"))
			{
				ReadSettingsValues();
			}
		}

		private void SetupDataGrid()
		{
			DataGrid.AutoGenerateColumns = false;
			DataGrid.AllowUserToAddRows = false;
			DataGrid.AllowUserToDeleteRows = false;

			profitEstimators = new BindingList<ProfitEstimator>();
			foreach(Recipe recipe in CalcSettings.DataHelper.Recipes)
			{
				//to exclude the dummy recipes for resource extraction buildings
				if(recipe.Outputs.Count > 0)
				{
					ProfitEstimator profitEstimator = new ProfitEstimator(CalcSettings, recipe);
					profitEstimators.Add(profitEstimator);
				}
				
			}

			DataGrid.DataSource = profitEstimators;

			CreateAndAddColumn("Building", "BuildingTicker");
			CreateAndAddColumn("Outputs", "OutputIdentifier");
			CreateAndAddColumn("Variant", "VariantIndicator");

			MakeColumnBold(CreateAndAddColumn("Total Profit", "CycleProfit", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE }));
			CreateAndAddColumn("Profit %", "PercentageProfit", new DataGridViewFormattedNullableNumberCell() { FormatString = "P2" });
			CreateAndAddColumn("Base Time", "BaseProductionTime", new DataGridViewFormattedNullableNumberCell() { FormatString = "N2" });
			CreateAndAddColumn("Speed Mod", "SpeedModifier", new DataGridViewFormattedNullableNumberCell() { FormatString = "P2" });
			CreateAndAddColumn("Adjusted Time", "AdjustedProductionTime", new DataGridViewFormattedNullableNumberCell() { FormatString = "N2" });
			MakeColumnBold(CreateAndAddColumn("Time Scaled Profit", "TimescaleProfit", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE }));

			CreateAndAddColumn("Materials", "MaterialCost", new DataGridViewFormattedNullableNumberCell() {FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Prod. Fee", "ProductionFee", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Consumables", "ConsumableCost", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Import", "ImportCost", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Export", "ExportCost", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Total Cost", "CycleCost", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			CreateAndAddColumn("Sale Value", "SalePrice", new DataGridViewFormattedNullableNumberCell() { FormatString = "C2", FormatProvider = FormatCultures.ACCOUNTING_CULTURE });
			
		}

		private DataGridViewColumn CreateAndAddColumn(string name, string propertyName = null, DataGridViewTextBoxCell cellTemplate = null)
		{
			if (propertyName == null) propertyName = name;
			DataGridViewColumn column = new DataGridViewTextBoxColumn()
			{
				DataPropertyName = propertyName,
				Name = name
			};

			DataGrid.Columns.Add(column);

			if (cellTemplate != null)
			{
				column.CellTemplate = cellTemplate;
			}

			return column;
		}

		private DataGridViewColumn MakeColumnBold(DataGridViewColumn column)
		{
			Font font = new Font(SystemFonts.DefaultFont, SystemFonts.DefaultFont.Style | FontStyle.Bold);
			column.DefaultCellStyle.Font = font;
			return column;
		}

		private void BindComboBox<T>(ComboBox comboBox, IEnumerable<T> dataSource, string displayMember)
		{
			comboBox.DataSource = dataSource.ToArray();
			comboBox.DisplayMember = displayMember;
			comboBox.BindingContext = new BindingContext();
		}

		private void CreateAndAddPopulationView(PopulationOptions populationOptions)
		{
			CheckBox checkBox = new CheckBox();
			CheckedListBox consumablesList = new ScrollsafeCheckedListBox();

			checkBox.Text = populationOptions.PopulationType.Name;
			checkBox.CheckedChanged += (object sender, EventArgs e) =>
			{
				consumablesList.Enabled = checkBox.Checked;
				populationOptions.Active = checkBox.Checked;
				RefreshGrid();
			};

			Dictionary<int, Material> indexMap = new Dictionary<int, Material>();
			consumablesList.Items.Clear();
			foreach (Material mat in populationOptions.GetRelevantMaterials())
			{
				indexMap.Add(consumablesList.Items.Add(mat.Ticker, false), mat);
			}
			consumablesList.CheckOnClick = true;
			consumablesList.ItemCheck += (object sender, ItemCheckEventArgs e) =>
			{
				if (!firstReadDone) return;
				populationOptions.SetEnabled(indexMap[e.Index], e.NewValue == CheckState.Checked);
				RefreshGrid();
			};
			consumablesList.SelectedIndexChanged += (object sender, EventArgs e) =>
			{
				consumablesList.ClearSelected();
			};
			
			//No I don't know why it has to be 1.5 times the height it should be, I assume it's a padding and rounding thing but honestly idk
			consumablesList.Size = new Size(consumablesList.Size.Width, (int)(consumablesList.Items.Count * consumablesList.GetItemHeight(0) * 1.5f));

			PopulationsFlowPanel.Controls.Add(checkBox);
			PopulationsFlowPanel.Controls.Add(consumablesList);
			PopulationControls.Add(populationOptions.PopulationType, new PopulationView(checkBox, consumablesList, indexMap));


		}

		//update settings values from the linked object, to be called whenever we load from file and during initialization
		private void ReadSettingsValues()
		{
			TimeScaleSelector.SelectedItem = CalcSettings.ProductionTimeScale;
			ExchangeSelector.SelectedItem = CalcSettings.Exchange;
			BuyModeSelector.SelectedItem = CalcSettings.BuyPriceMode;
			SellModeSelector.SelectedItem = CalcSettings.SellPriceMode;
			ConsumablesModeSelector.SelectedItem = CalcSettings.ConsumablesPriceMode;
			FuelModeSelector.SelectedItem = CalcSettings.FuelPriceMode;

			foreach(string expertise in Building.EXPERTISE_ALL)
			{
				ExpertSelectors[expertise].Value = CalcSettings.GetExpertsForExpertise(expertise);
				FeeSelectors[expertise].Value = CalcSettings.GetBaseProductionFee(expertise);
			}

			foreach(var pair in PopulationControls)
			{
				PopulationOptions populationOptions = CalcSettings.PopulationSettings[pair.Key];
				foreach(int i in pair.Value.IndexMap.Keys)
				{
					pair.Value.Consumables.SetItemChecked(i, populationOptions.GetEnabled(pair.Value.IndexMap[i]));
				}
				pair.Value.Enabled.Checked = populationOptions.Active;
			}
			

			ImportSFNumeric.Value = CalcSettings.ImportSF;
			ImportFFNumeric.Value = CalcSettings.ImportFF;
			ExportSFNumeric.Value = CalcSettings.ExportSF;
			ExportFFNumeric.Value = CalcSettings.ExportFF;
			ShipBoundingSelector.SelectedItem = CalcSettings.ShippingCostMode;
			MaxMassSelector.Value = CalcSettings.ShipMassCapacity;
			MaxVolumeSelector.Value = CalcSettings.ShipVolumeCapacity;
			FillMarginSelector.Value = CalcSettings.ShipFillMargin * 100;//account for percentiles
			IgnoreCommoditiesLabel.Text = $"Shipping costs are ingored for {CalcSettings.IgnoreShipping.Count} commodities.";


			firstReadDone = true;

			if (editCommodityListForm != null)
			{
				editCommodityListForm.RefreshFromList();
			}

			RefreshGrid();
		}

		#region auto assignment event handlers
		private void TimeScaleChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ProductionTimeScale = (TimeScale)TimeScaleSelector.SelectedItem;
			RefreshGrid();
		}

		private void ExchangeChanged(object sender, EventArgs e)
		{
			CalcSettings.Exchange = (ExchangeData)ExchangeSelector.SelectedItem;
			RefreshGrid();
		}

		private void BuyModeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.BuyPriceMode = (PriceMode)BuyModeSelector.SelectedItem;
			RefreshGrid();
		}
		
		private void SellModeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.SellPriceMode = (PriceMode)SellModeSelector.SelectedItem;
			RefreshGrid();
		}

		private void ConsumablesModeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ConsumablesPriceMode = (PriceMode)ConsumablesModeSelector.SelectedItem;
			RefreshGrid();
		}

		private void FuelModeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.FuelPriceMode = (PriceMode)FuelModeSelector.SelectedItem;
			RefreshGrid();
		}

		private void ImportSFChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ImportSF = (int)ImportSFNumeric.Value;
			RefreshGrid();
		}

		private void ExportSFChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ExportSF = (int)ExportSFNumeric.Value;
			RefreshGrid();
		}

		private void ImportFFChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ImportFF = (int)ImportFFNumeric.Value;
			RefreshGrid();
		}

		private void ExportFFChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ExportFF = (int)ExportFFNumeric.Value;
			RefreshGrid();
		}

		private void BoundingModeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ShippingCostMode = (ShippingCostBoundingMode)ShipBoundingSelector.SelectedItem;
			RefreshGrid();
		}

		private void MaxMassChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ShipMassCapacity = (int)MaxMassSelector.Value;
			RefreshGrid();
		}

		private void MaxVolumeChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ShipVolumeCapacity = (int)MaxVolumeSelector.Value;
			RefreshGrid();
		}

		private void FillMarginChanged(object sender, EventArgs e)
		{
			if (!firstReadDone) return;
			CalcSettings.ShipFillMargin = FillMarginSelector.Value / 100;//account for percentiles
			RefreshGrid();
		}
		#endregion

		private void TogglePopulationOptions(object sender, EventArgs e)
		{
			PopulationsFlowPanel.Visible = !PopulationsFlowPanel.Visible;
			PopulationsVisibilityToggle.Image = PopulationsFlowPanel.Visible ? Properties.Resources.CollapseUp_16x : Properties.Resources.ExpandDown_16x;
			
		}

		private void ToggleSettingsPanel(object sender, EventArgs e)
		{
			Settings.Visible = !Settings.Visible;
			MainContainer.SplitterDistance = Settings.Visible ? expandedSplitterDistance : collpasedSplitterDistance;
		}

		private void LoadPreset(object sender, EventArgs e)
		{
			new SaveOrLoadPresetDialog(CalculatorPresetsDir, true, ReadPresetFile).ShowDialog();
		}

		private void SavePreset(object sender, EventArgs e)
		{
			new SaveOrLoadPresetDialog(CalculatorPresetsDir, false, WritePresetFile).ShowDialog();
		}

		private bool ReadPresetFile(string filePath)
		{
			if (!File.Exists(filePath)) return false;
			using(StreamReader streamReader = File.OpenText(filePath))
			{
				using JsonTextReader jsonReader = new JsonTextReader(streamReader);
				JObject jObject = (JObject)JToken.ReadFrom(jsonReader);
				try
				{
					CalcSettings.FromJson(jObject);
				}
				finally
				{
					ReadSettingsValues();
				}
			}
			return true;
		}

		private bool WritePresetFile(string filePath)
		{
			using (StreamWriter streamWriter= File.CreateText(filePath))
			{
				using JsonTextWriter jsonWriter = new JsonTextWriter(streamWriter);
				jsonWriter.Formatting = Formatting.Indented;
				CalcSettings.ToJson().WriteTo(jsonWriter);
			}
			return true;
		}

		private void EditIgnoredCommodities(object sender, EventArgs e)
		{
			if(editCommodityListForm == null)
			{
				editCommodityListForm = new EditCommodityListForm(CalcSettings.DataHelper, CalcSettings.IgnoreShipping);
				editCommodityListForm.OnItemSelected += (mat) => RefreshGrid();
				editCommodityListForm.OnItemDeselected += (mat) => RefreshGrid();
			}
			editCommodityListForm.Show();

		}

		//re-calculate the grid after after a settings change or new market data
		//Because our underlying types can't be trusted to prompt updates we need to induce them properly
		private void RefreshGrid()
		{
			DataGrid.Update();
			DataGrid.Refresh();
		}

		//TODO:seriously give this an optimisation pass, this is, by an enormous margin, the most expensive operation when the ui redraws
		//redrawing the rows is, at this point, a massive slowdown and usability issue, the entire application locks for a second or two while it redraws
		//it might be worth abandoning the gradients entirely in the name of expedience
		//caching brushes on a per-column basis might help?
		//I'm still not entirely certain if it's the creation and disposal of the brushes that has the performance cost or if it's the draw operations themselves
		//further testing is needed
		//that said work on something else for a while. You've already driven yourself mad 3 times on this (remember to update the next time you fuck things up)
		private void DataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{

			
			if (e.RowIndex < 0) return;
			ProfitEstimator est = (ProfitEstimator)DataGrid.Rows[e.RowIndex].DataBoundItem;
			ColorGroup colorGroup = ColorGroup.GetColor(est.Building);
			using (var brush = colorGroup.CreateLinearGradientBrush(e.CellBounds))
			{
				e.Graphics.FillRectangle(brush, e.CellBounds);
			}
			using (var brush = new SolidBrush(DataGrid.GridColor))
			{
				//draw our dridlines
				using Pen gridLinePen = new Pen(brush);
				e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
					e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
					e.CellBounds.Bottom - 1);
				e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
					e.CellBounds.Top, e.CellBounds.Right - 1,
					e.CellBounds.Bottom);
			}
			

			using (var brush = new SolidBrush(colorGroup.Text))
			{
				e.Graphics.DrawString((string)e.FormattedValue, e.CellStyle.Font, brush, e.CellBounds.X + 2, e.CellBounds.Y + 2);
			}

			if (DataGrid.SelectedRows.Cast<DataGridViewRow>().Any(row => row.Index == e.RowIndex))
			{
				//this row is selected, overlay a selection box
				Point topLeft = new Point(e.CellBounds.X, e.CellBounds.Y + 1);
				Point topRight = new Point(e.CellBounds.X + e.CellBounds.Width, e.CellBounds.Y + 1);
				Point bottomLeft = new Point(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - 4);
				Point bottomRight = new Point(e.CellBounds.X + e.CellBounds.Width, e.CellBounds.Y + e.CellBounds.Height - 4);
				if(e.ColumnIndex == 0)
				{
					//draw our left bound
					topLeft.X += 1;
					bottomLeft.X += 1;
					e.Graphics.DrawLine(Pens.Blue, topLeft, bottomLeft);
				}
				if(e.ColumnIndex == DataGrid.Columns.Count - 1)
				{
					topRight.X -= 4;
					bottomRight.X -= 4;
					//draw our right bound
					e.Graphics.DrawLine(Pens.Blue, topRight, bottomRight);
				}

				//draw our top and bottom bounds
				e.Graphics.DrawLine(Pens.Blue, topLeft, topRight);
				e.Graphics.DrawLine(Pens.Blue, bottomLeft, bottomRight);

			}

			e.Handled = true;
			
		}

		private void ProfitEstimatorView_SizeChanged(object sender, EventArgs e)
		{
			MainContainer.SplitterDistance = Settings.Visible ? expandedSplitterDistance : collpasedSplitterDistance;
		}

		private void ProfitEstimatorView_Load(object sender, EventArgs e)
		{
			MainContainer.SplitterDistance = Settings.Visible ? expandedSplitterDistance : collpasedSplitterDistance;
		}

		//save the current state for loading later
		public void SaveState()
		{
			WritePresetFile($"{CalculatorPresetsDir}\\{ProsperousAssistant.Settings.LastSettingPath}");
		}

		private void ProfitEstimatorView_VisibleChanged(object sender, EventArgs e)
		{
			//this blocks the display thread updating, but I don't want to spin it off into it's own thread because the things we're fiddling with are decidedly *not* thread safe
			SaveState();
		}
	}
}
