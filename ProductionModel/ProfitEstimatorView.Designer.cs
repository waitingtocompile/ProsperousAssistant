namespace ProsperousAssistant.ProductionModel
{
	partial class ProfitEstimatorView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			//custom bit because we have a child form we need to clean up
			if (editCommodityListForm != null)
			{
				editCommodityListForm.Hide();
				editCommodityListForm.Dispose();
			}

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.DataGrid = new System.Windows.Forms.DataGridView();
			this.Settings = new System.Windows.Forms.TabControl();
			this.CostsTab = new System.Windows.Forms.TabPage();
			this.CostsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.CostsHeaderPanel = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.ExchangeSelector = new System.Windows.Forms.ComboBox();
			this.LoadPresetButton = new System.Windows.Forms.Button();
			this.SavePresetButton = new System.Windows.Forms.Button();
			this.TimeScaleSelector = new System.Windows.Forms.ComboBox();
			this.TimeScaleLabel = new System.Windows.Forms.Label();
			this.PriceModesPanel = new System.Windows.Forms.Panel();
			this.BuyModeLabel = new System.Windows.Forms.Label();
			this.PriceModesLabel = new System.Windows.Forms.Label();
			this.BuyModeSelector = new System.Windows.Forms.ComboBox();
			this.SellModeLabel = new System.Windows.Forms.Label();
			this.SellModeSelector = new System.Windows.Forms.ComboBox();
			this.FuelModeSelector = new System.Windows.Forms.ComboBox();
			this.ConsumablesModeLabel = new System.Windows.Forms.Label();
			this.FuelModeLabel = new System.Windows.Forms.Label();
			this.ConsumablesModeSelector = new System.Windows.Forms.ComboBox();
			this.ExpertisePanel = new System.Windows.Forms.Panel();
			this.ExpertiseAndFeesTable = new System.Windows.Forms.TableLayoutPanel();
			this.FieldHeading = new System.Windows.Forms.Label();
			this.ProdFeeHeading = new System.Windows.Forms.Label();
			this.ExpertsHeading = new System.Windows.Forms.Label();
			this.ExpertiseAndFeesLabel = new System.Windows.Forms.Label();
			this.PopulationsPanel = new System.Windows.Forms.Panel();
			this.PopulationsAndConsumablesLabel = new System.Windows.Forms.Label();
			this.PopulationsVisibilityToggle = new System.Windows.Forms.Button();
			this.PopulationsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.ShippingSettingsPanel = new System.Windows.Forms.Panel();
			this.IgnoreCommoditiesLabel = new System.Windows.Forms.Label();
			this.EditIgnoreButton = new System.Windows.Forms.Button();
			this.FillMarginSelector = new ProsperousAssistant.Util.PercentageUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.MaxVolumeSelector = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.MaxMassSelector = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.ShipBoundingSelector = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ImportSFNumeric = new System.Windows.Forms.NumericUpDown();
			this.ImportFFNumeric = new System.Windows.Forms.NumericUpDown();
			this.ExportSFNumeric = new System.Windows.Forms.NumericUpDown();
			this.ExportFFNumeric = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.IgnoreImport = new System.Windows.Forms.CheckBox();
			this.IgnoreExport = new System.Windows.Forms.CheckBox();
			this.ShippingLabel = new System.Windows.Forms.Label();
			this.ViewTab = new System.Windows.Forms.TabPage();
			this.MainContainer = new System.Windows.Forms.SplitContainer();
			this.SettingsToggleButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
			this.Settings.SuspendLayout();
			this.CostsTab.SuspendLayout();
			this.CostsFlowPanel.SuspendLayout();
			this.CostsHeaderPanel.SuspendLayout();
			this.PriceModesPanel.SuspendLayout();
			this.ExpertisePanel.SuspendLayout();
			this.ExpertiseAndFeesTable.SuspendLayout();
			this.PopulationsPanel.SuspendLayout();
			this.ShippingSettingsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FillMarginSelector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxVolumeSelector)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxMassSelector)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImportSFNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImportFFNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ExportSFNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ExportFFNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
			this.MainContainer.Panel1.SuspendLayout();
			this.MainContainer.Panel2.SuspendLayout();
			this.MainContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// DataGrid
			// 
			this.DataGrid.AllowUserToAddRows = false;
			this.DataGrid.AllowUserToDeleteRows = false;
			this.DataGrid.AllowUserToOrderColumns = true;
			this.DataGrid.AllowUserToResizeRows = false;
			this.DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataGrid.Location = new System.Drawing.Point(0, 0);
			this.DataGrid.Margin = new System.Windows.Forms.Padding(0);
			this.DataGrid.Name = "DataGrid";
			this.DataGrid.ReadOnly = true;
			this.DataGrid.RowHeadersVisible = false;
			this.DataGrid.RowTemplate.Height = 25;
			this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DataGrid.Size = new System.Drawing.Size(712, 670);
			this.DataGrid.TabIndex = 0;
			this.DataGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGrid_CellPainting);
			this.DataGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGrid_RowPrePaint);
			// 
			// Settings
			// 
			this.Settings.Controls.Add(this.CostsTab);
			this.Settings.Controls.Add(this.ViewTab);
			this.Settings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Settings.Location = new System.Drawing.Point(0, 0);
			this.Settings.Margin = new System.Windows.Forms.Padding(0);
			this.Settings.Name = "Settings";
			this.Settings.SelectedIndex = 0;
			this.Settings.Size = new System.Drawing.Size(324, 670);
			this.Settings.TabIndex = 1;
			// 
			// CostsTab
			// 
			this.CostsTab.AutoScroll = true;
			this.CostsTab.Controls.Add(this.CostsFlowPanel);
			this.CostsTab.Location = new System.Drawing.Point(4, 24);
			this.CostsTab.Name = "CostsTab";
			this.CostsTab.Padding = new System.Windows.Forms.Padding(3);
			this.CostsTab.Size = new System.Drawing.Size(316, 642);
			this.CostsTab.TabIndex = 0;
			this.CostsTab.Text = "Costs";
			this.CostsTab.UseVisualStyleBackColor = true;
			// 
			// CostsFlowPanel
			// 
			this.CostsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CostsFlowPanel.AutoSize = true;
			this.CostsFlowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CostsFlowPanel.Controls.Add(this.CostsHeaderPanel);
			this.CostsFlowPanel.Controls.Add(this.PriceModesPanel);
			this.CostsFlowPanel.Controls.Add(this.ExpertisePanel);
			this.CostsFlowPanel.Controls.Add(this.PopulationsPanel);
			this.CostsFlowPanel.Controls.Add(this.PopulationsFlowPanel);
			this.CostsFlowPanel.Controls.Add(this.ShippingSettingsPanel);
			this.CostsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.CostsFlowPanel.Location = new System.Drawing.Point(-4, 0);
			this.CostsFlowPanel.Margin = new System.Windows.Forms.Padding(0);
			this.CostsFlowPanel.Name = "CostsFlowPanel";
			this.CostsFlowPanel.Size = new System.Drawing.Size(312, 630);
			this.CostsFlowPanel.TabIndex = 20;
			this.CostsFlowPanel.WrapContents = false;
			// 
			// CostsHeaderPanel
			// 
			this.CostsHeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CostsHeaderPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CostsHeaderPanel.Controls.Add(this.label9);
			this.CostsHeaderPanel.Controls.Add(this.ExchangeSelector);
			this.CostsHeaderPanel.Controls.Add(this.LoadPresetButton);
			this.CostsHeaderPanel.Controls.Add(this.SavePresetButton);
			this.CostsHeaderPanel.Controls.Add(this.TimeScaleSelector);
			this.CostsHeaderPanel.Controls.Add(this.TimeScaleLabel);
			this.CostsHeaderPanel.Location = new System.Drawing.Point(0, 0);
			this.CostsHeaderPanel.Margin = new System.Windows.Forms.Padding(0);
			this.CostsHeaderPanel.Name = "CostsHeaderPanel";
			this.CostsHeaderPanel.Size = new System.Drawing.Size(312, 92);
			this.CostsHeaderPanel.TabIndex = 0;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 68);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(125, 15);
			this.label9.TabIndex = 6;
			this.label9.Text = "Commodity Exchange";
			// 
			// ExchangeSelector
			// 
			this.ExchangeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ExchangeSelector.FormattingEnabled = true;
			this.ExchangeSelector.Location = new System.Drawing.Point(170, 65);
			this.ExchangeSelector.Name = "ExchangeSelector";
			this.ExchangeSelector.Size = new System.Drawing.Size(119, 23);
			this.ExchangeSelector.TabIndex = 5;
			this.ExchangeSelector.SelectedIndexChanged += new System.EventHandler(this.ExchangeChanged);
			// 
			// LoadPresetButton
			// 
			this.LoadPresetButton.Location = new System.Drawing.Point(7, 6);
			this.LoadPresetButton.Name = "LoadPresetButton";
			this.LoadPresetButton.Size = new System.Drawing.Size(85, 23);
			this.LoadPresetButton.TabIndex = 0;
			this.LoadPresetButton.Text = "Load Preset";
			this.LoadPresetButton.UseVisualStyleBackColor = true;
			this.LoadPresetButton.Click += new System.EventHandler(this.LoadPreset);
			// 
			// SavePresetButton
			// 
			this.SavePresetButton.Location = new System.Drawing.Point(98, 6);
			this.SavePresetButton.Name = "SavePresetButton";
			this.SavePresetButton.Size = new System.Drawing.Size(75, 23);
			this.SavePresetButton.TabIndex = 1;
			this.SavePresetButton.Text = "Save Preset";
			this.SavePresetButton.UseVisualStyleBackColor = true;
			this.SavePresetButton.Click += new System.EventHandler(this.SavePreset);
			// 
			// TimeScaleSelector
			// 
			this.TimeScaleSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TimeScaleSelector.FormattingEnabled = true;
			this.TimeScaleSelector.Location = new System.Drawing.Point(170, 35);
			this.TimeScaleSelector.Name = "TimeScaleSelector";
			this.TimeScaleSelector.Size = new System.Drawing.Size(119, 23);
			this.TimeScaleSelector.TabIndex = 4;
			this.TimeScaleSelector.SelectedIndexChanged += new System.EventHandler(this.TimeScaleChanged);
			// 
			// TimeScaleLabel
			// 
			this.TimeScaleLabel.AutoSize = true;
			this.TimeScaleLabel.Location = new System.Drawing.Point(7, 38);
			this.TimeScaleLabel.Name = "TimeScaleLabel";
			this.TimeScaleLabel.Size = new System.Drawing.Size(125, 15);
			this.TimeScaleLabel.TabIndex = 3;
			this.TimeScaleLabel.Text = "Production Time Scale";
			// 
			// PriceModesPanel
			// 
			this.PriceModesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PriceModesPanel.AutoSize = true;
			this.PriceModesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PriceModesPanel.Controls.Add(this.BuyModeLabel);
			this.PriceModesPanel.Controls.Add(this.PriceModesLabel);
			this.PriceModesPanel.Controls.Add(this.BuyModeSelector);
			this.PriceModesPanel.Controls.Add(this.SellModeLabel);
			this.PriceModesPanel.Controls.Add(this.SellModeSelector);
			this.PriceModesPanel.Controls.Add(this.FuelModeSelector);
			this.PriceModesPanel.Controls.Add(this.ConsumablesModeLabel);
			this.PriceModesPanel.Controls.Add(this.FuelModeLabel);
			this.PriceModesPanel.Controls.Add(this.ConsumablesModeSelector);
			this.PriceModesPanel.Location = new System.Drawing.Point(0, 92);
			this.PriceModesPanel.Margin = new System.Windows.Forms.Padding(0);
			this.PriceModesPanel.Name = "PriceModesPanel";
			this.PriceModesPanel.Size = new System.Drawing.Size(312, 140);
			this.PriceModesPanel.TabIndex = 1;
			// 
			// BuyModeLabel
			// 
			this.BuyModeLabel.AutoSize = true;
			this.BuyModeLabel.Location = new System.Drawing.Point(7, 30);
			this.BuyModeLabel.Name = "BuyModeLabel";
			this.BuyModeLabel.Size = new System.Drawing.Size(27, 15);
			this.BuyModeLabel.TabIndex = 6;
			this.BuyModeLabel.Text = "Buy";
			// 
			// PriceModesLabel
			// 
			this.PriceModesLabel.AutoSize = true;
			this.PriceModesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PriceModesLabel.Location = new System.Drawing.Point(7, 3);
			this.PriceModesLabel.Name = "PriceModesLabel";
			this.PriceModesLabel.Size = new System.Drawing.Size(84, 19);
			this.PriceModesLabel.TabIndex = 5;
			this.PriceModesLabel.Text = "Price Modes";
			// 
			// BuyModeSelector
			// 
			this.BuyModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BuyModeSelector.FormattingEnabled = true;
			this.BuyModeSelector.Location = new System.Drawing.Point(171, 27);
			this.BuyModeSelector.Name = "BuyModeSelector";
			this.BuyModeSelector.Size = new System.Drawing.Size(119, 23);
			this.BuyModeSelector.TabIndex = 7;
			this.BuyModeSelector.SelectedValueChanged += new System.EventHandler(this.BuyModeChanged);
			// 
			// SellModeLabel
			// 
			this.SellModeLabel.AutoSize = true;
			this.SellModeLabel.Location = new System.Drawing.Point(7, 59);
			this.SellModeLabel.Name = "SellModeLabel";
			this.SellModeLabel.Size = new System.Drawing.Size(25, 15);
			this.SellModeLabel.TabIndex = 8;
			this.SellModeLabel.Text = "Sell";
			// 
			// SellModeSelector
			// 
			this.SellModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SellModeSelector.FormattingEnabled = true;
			this.SellModeSelector.Location = new System.Drawing.Point(171, 56);
			this.SellModeSelector.Name = "SellModeSelector";
			this.SellModeSelector.Size = new System.Drawing.Size(119, 23);
			this.SellModeSelector.TabIndex = 9;
			this.SellModeSelector.SelectedValueChanged += new System.EventHandler(this.SellModeChanged);
			// 
			// FuelModeSelector
			// 
			this.FuelModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FuelModeSelector.FormattingEnabled = true;
			this.FuelModeSelector.Location = new System.Drawing.Point(171, 114);
			this.FuelModeSelector.Name = "FuelModeSelector";
			this.FuelModeSelector.Size = new System.Drawing.Size(119, 23);
			this.FuelModeSelector.TabIndex = 13;
			this.FuelModeSelector.SelectedValueChanged += new System.EventHandler(this.FuelModeChanged);
			// 
			// ConsumablesModeLabel
			// 
			this.ConsumablesModeLabel.AutoSize = true;
			this.ConsumablesModeLabel.Location = new System.Drawing.Point(7, 88);
			this.ConsumablesModeLabel.Name = "ConsumablesModeLabel";
			this.ConsumablesModeLabel.Size = new System.Drawing.Size(79, 15);
			this.ConsumablesModeLabel.TabIndex = 10;
			this.ConsumablesModeLabel.Text = "Consumables";
			// 
			// FuelModeLabel
			// 
			this.FuelModeLabel.AutoSize = true;
			this.FuelModeLabel.Location = new System.Drawing.Point(7, 117);
			this.FuelModeLabel.Name = "FuelModeLabel";
			this.FuelModeLabel.Size = new System.Drawing.Size(29, 15);
			this.FuelModeLabel.TabIndex = 12;
			this.FuelModeLabel.Text = "Fuel";
			// 
			// ConsumablesModeSelector
			// 
			this.ConsumablesModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ConsumablesModeSelector.FormattingEnabled = true;
			this.ConsumablesModeSelector.Location = new System.Drawing.Point(171, 85);
			this.ConsumablesModeSelector.Name = "ConsumablesModeSelector";
			this.ConsumablesModeSelector.Size = new System.Drawing.Size(119, 23);
			this.ConsumablesModeSelector.TabIndex = 11;
			this.ConsumablesModeSelector.SelectedValueChanged += new System.EventHandler(this.ConsumablesModeChanged);
			// 
			// ExpertisePanel
			// 
			this.ExpertisePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ExpertisePanel.AutoSize = true;
			this.ExpertisePanel.Controls.Add(this.ExpertiseAndFeesTable);
			this.ExpertisePanel.Controls.Add(this.ExpertiseAndFeesLabel);
			this.ExpertisePanel.Location = new System.Drawing.Point(3, 235);
			this.ExpertisePanel.Name = "ExpertisePanel";
			this.ExpertisePanel.Size = new System.Drawing.Size(306, 46);
			this.ExpertisePanel.TabIndex = 18;
			// 
			// ExpertiseAndFeesTable
			// 
			this.ExpertiseAndFeesTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ExpertiseAndFeesTable.AutoSize = true;
			this.ExpertiseAndFeesTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.ExpertiseAndFeesTable.ColumnCount = 3;
			this.ExpertiseAndFeesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
			this.ExpertiseAndFeesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
			this.ExpertiseAndFeesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
			this.ExpertiseAndFeesTable.Controls.Add(this.FieldHeading, 0, 0);
			this.ExpertiseAndFeesTable.Controls.Add(this.ProdFeeHeading, 1, 0);
			this.ExpertiseAndFeesTable.Controls.Add(this.ExpertsHeading, 2, 0);
			this.ExpertiseAndFeesTable.Location = new System.Drawing.Point(3, 26);
			this.ExpertiseAndFeesTable.Name = "ExpertiseAndFeesTable";
			this.ExpertiseAndFeesTable.RowCount = 1;
			this.ExpertiseAndFeesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.ExpertiseAndFeesTable.Size = new System.Drawing.Size(300, 17);
			this.ExpertiseAndFeesTable.TabIndex = 18;
			// 
			// FieldHeading
			// 
			this.FieldHeading.AutoSize = true;
			this.FieldHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.FieldHeading.Location = new System.Drawing.Point(4, 1);
			this.FieldHeading.Name = "FieldHeading";
			this.FieldHeading.Size = new System.Drawing.Size(33, 15);
			this.FieldHeading.TabIndex = 0;
			this.FieldHeading.Text = "Field";
			// 
			// ProdFeeHeading
			// 
			this.ProdFeeHeading.AutoSize = true;
			this.ProdFeeHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.ProdFeeHeading.Location = new System.Drawing.Point(160, 1);
			this.ProdFeeHeading.Name = "ProdFeeHeading";
			this.ProdFeeHeading.Size = new System.Drawing.Size(59, 15);
			this.ProdFeeHeading.TabIndex = 2;
			this.ProdFeeHeading.Text = "Prod. Fee";
			// 
			// ExpertsHeading
			// 
			this.ExpertsHeading.AutoSize = true;
			this.ExpertsHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.ExpertsHeading.Location = new System.Drawing.Point(226, 1);
			this.ExpertsHeading.Name = "ExpertsHeading";
			this.ExpertsHeading.Size = new System.Drawing.Size(49, 15);
			this.ExpertsHeading.TabIndex = 1;
			this.ExpertsHeading.Text = "Experts\r\n";
			// 
			// ExpertiseAndFeesLabel
			// 
			this.ExpertiseAndFeesLabel.AutoSize = true;
			this.ExpertiseAndFeesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ExpertiseAndFeesLabel.Location = new System.Drawing.Point(4, 4);
			this.ExpertiseAndFeesLabel.Name = "ExpertiseAndFeesLabel";
			this.ExpertiseAndFeesLabel.Size = new System.Drawing.Size(121, 19);
			this.ExpertiseAndFeesLabel.TabIndex = 16;
			this.ExpertiseAndFeesLabel.Text = "Expertise and Fees";
			// 
			// PopulationsPanel
			// 
			this.PopulationsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PopulationsPanel.Controls.Add(this.PopulationsAndConsumablesLabel);
			this.PopulationsPanel.Controls.Add(this.PopulationsVisibilityToggle);
			this.PopulationsPanel.Location = new System.Drawing.Point(3, 287);
			this.PopulationsPanel.Name = "PopulationsPanel";
			this.PopulationsPanel.Size = new System.Drawing.Size(284, 28);
			this.PopulationsPanel.TabIndex = 19;
			// 
			// PopulationsAndConsumablesLabel
			// 
			this.PopulationsAndConsumablesLabel.AutoSize = true;
			this.PopulationsAndConsumablesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.PopulationsAndConsumablesLabel.Location = new System.Drawing.Point(4, 6);
			this.PopulationsAndConsumablesLabel.Name = "PopulationsAndConsumablesLabel";
			this.PopulationsAndConsumablesLabel.Size = new System.Drawing.Size(193, 19);
			this.PopulationsAndConsumablesLabel.TabIndex = 0;
			this.PopulationsAndConsumablesLabel.Text = "Populations and Consumables";
			// 
			// PopulationsVisibilityToggle
			// 
			this.PopulationsVisibilityToggle.Image = global::ProsperousAssistant.Properties.Resources.CollapseUp_16x;
			this.PopulationsVisibilityToggle.Location = new System.Drawing.Point(261, 5);
			this.PopulationsVisibilityToggle.Name = "PopulationsVisibilityToggle";
			this.PopulationsVisibilityToggle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.PopulationsVisibilityToggle.Size = new System.Drawing.Size(20, 20);
			this.PopulationsVisibilityToggle.TabIndex = 20;
			this.PopulationsVisibilityToggle.UseVisualStyleBackColor = true;
			this.PopulationsVisibilityToggle.Click += new System.EventHandler(this.TogglePopulationOptions);
			// 
			// PopulationsFlowPanel
			// 
			this.PopulationsFlowPanel.AutoSize = true;
			this.PopulationsFlowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PopulationsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.PopulationsFlowPanel.Location = new System.Drawing.Point(3, 321);
			this.PopulationsFlowPanel.Name = "PopulationsFlowPanel";
			this.PopulationsFlowPanel.Size = new System.Drawing.Size(0, 0);
			this.PopulationsFlowPanel.TabIndex = 1;
			this.PopulationsFlowPanel.WrapContents = false;
			// 
			// ShippingSettingsPanel
			// 
			this.ShippingSettingsPanel.AutoSize = true;
			this.ShippingSettingsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ShippingSettingsPanel.Controls.Add(this.IgnoreCommoditiesLabel);
			this.ShippingSettingsPanel.Controls.Add(this.EditIgnoreButton);
			this.ShippingSettingsPanel.Controls.Add(this.FillMarginSelector);
			this.ShippingSettingsPanel.Controls.Add(this.label8);
			this.ShippingSettingsPanel.Controls.Add(this.label7);
			this.ShippingSettingsPanel.Controls.Add(this.MaxVolumeSelector);
			this.ShippingSettingsPanel.Controls.Add(this.label6);
			this.ShippingSettingsPanel.Controls.Add(this.MaxMassSelector);
			this.ShippingSettingsPanel.Controls.Add(this.label5);
			this.ShippingSettingsPanel.Controls.Add(this.ShipBoundingSelector);
			this.ShippingSettingsPanel.Controls.Add(this.tableLayoutPanel1);
			this.ShippingSettingsPanel.Controls.Add(this.ShippingLabel);
			this.ShippingSettingsPanel.Location = new System.Drawing.Point(3, 327);
			this.ShippingSettingsPanel.Name = "ShippingSettingsPanel";
			this.ShippingSettingsPanel.Size = new System.Drawing.Size(284, 300);
			this.ShippingSettingsPanel.TabIndex = 21;
			// 
			// IgnoreCommoditiesLabel
			// 
			this.IgnoreCommoditiesLabel.Location = new System.Drawing.Point(8, 266);
			this.IgnoreCommoditiesLabel.Name = "IgnoreCommoditiesLabel";
			this.IgnoreCommoditiesLabel.Size = new System.Drawing.Size(195, 34);
			this.IgnoreCommoditiesLabel.TabIndex = 14;
			this.IgnoreCommoditiesLabel.Text = "label10\r\nthing\r\n";
			// 
			// EditIgnoreButton
			// 
			this.EditIgnoreButton.Location = new System.Drawing.Point(206, 269);
			this.EditIgnoreButton.Name = "EditIgnoreButton";
			this.EditIgnoreButton.Size = new System.Drawing.Size(75, 23);
			this.EditIgnoreButton.TabIndex = 13;
			this.EditIgnoreButton.Text = "Edit";
			this.EditIgnoreButton.UseVisualStyleBackColor = true;
			this.EditIgnoreButton.Click += new System.EventHandler(this.EditIgnoredCommodities);
			// 
			// FillMarginSelector
			// 
			this.FillMarginSelector.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.FillMarginSelector.Location = new System.Drawing.Point(160, 240);
			this.FillMarginSelector.Name = "FillMarginSelector";
			this.FillMarginSelector.Size = new System.Drawing.Size(120, 23);
			this.FillMarginSelector.TabIndex = 12;
			this.FillMarginSelector.ValueChanged += new System.EventHandler(this.FillMarginChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 242);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(89, 15);
			this.label8.TabIndex = 11;
			this.label8.Text = "Ship Fill Margin";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 213);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(122, 15);
			this.label7.TabIndex = 9;
			this.label7.Tag = "";
			this.label7.Text = "Ship Volume Capacity";
			// 
			// MaxVolumeSelector
			// 
			this.MaxVolumeSelector.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.MaxVolumeSelector.Location = new System.Drawing.Point(160, 211);
			this.MaxVolumeSelector.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.MaxVolumeSelector.Name = "MaxVolumeSelector";
			this.MaxVolumeSelector.Size = new System.Drawing.Size(120, 23);
			this.MaxVolumeSelector.TabIndex = 8;
			this.MaxVolumeSelector.ThousandsSeparator = true;
			this.MaxVolumeSelector.ValueChanged += new System.EventHandler(this.MaxVolumeChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(109, 15);
			this.label6.TabIndex = 7;
			this.label6.Text = "Ship Mass Capacity";
			// 
			// MaxMassSelector
			// 
			this.MaxMassSelector.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.MaxMassSelector.Location = new System.Drawing.Point(160, 182);
			this.MaxMassSelector.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.MaxMassSelector.Name = "MaxMassSelector";
			this.MaxMassSelector.Size = new System.Drawing.Size(120, 23);
			this.MaxMassSelector.TabIndex = 6;
			this.MaxMassSelector.ThousandsSeparator = true;
			this.MaxMassSelector.ValueChanged += new System.EventHandler(this.MaxMassChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(142, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Capacity Bounding Mode";
			// 
			// ShipBoundingSelector
			// 
			this.ShipBoundingSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ShipBoundingSelector.FormattingEnabled = true;
			this.ShipBoundingSelector.Location = new System.Drawing.Point(160, 153);
			this.ShipBoundingSelector.Name = "ShipBoundingSelector";
			this.ShipBoundingSelector.Size = new System.Drawing.Size(121, 23);
			this.ShipBoundingSelector.TabIndex = 4;
			this.ShipBoundingSelector.SelectedValueChanged += new System.EventHandler(this.BoundingModeChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.ImportSFNumeric, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.ImportFFNumeric, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.ExportSFNumeric, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.ExportFFNumeric, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.IgnoreImport, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.IgnoreExport, 2, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 31);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(276, 113);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 29);
			this.label2.TabIndex = 1;
			this.label2.Text = "SF";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 59);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 29);
			this.label3.TabIndex = 2;
			this.label3.Text = "FF";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ImportSFNumeric
			// 
			this.ImportSFNumeric.Location = new System.Drawing.Point(47, 33);
			this.ImportSFNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ImportSFNumeric.Name = "ImportSFNumeric";
			this.ImportSFNumeric.Size = new System.Drawing.Size(110, 23);
			this.ImportSFNumeric.TabIndex = 3;
			this.ImportSFNumeric.ValueChanged += new System.EventHandler(this.ImportSFChanged);
			// 
			// ImportFFNumeric
			// 
			this.ImportFFNumeric.Location = new System.Drawing.Point(47, 62);
			this.ImportFFNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ImportFFNumeric.Name = "ImportFFNumeric";
			this.ImportFFNumeric.Size = new System.Drawing.Size(110, 23);
			this.ImportFFNumeric.TabIndex = 4;
			this.ImportFFNumeric.ValueChanged += new System.EventHandler(this.ImportFFChanged);
			// 
			// ExportSFNumeric
			// 
			this.ExportSFNumeric.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ExportSFNumeric.Location = new System.Drawing.Point(163, 33);
			this.ExportSFNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ExportSFNumeric.Name = "ExportSFNumeric";
			this.ExportSFNumeric.Size = new System.Drawing.Size(110, 23);
			this.ExportSFNumeric.TabIndex = 5;
			this.ExportSFNumeric.ValueChanged += new System.EventHandler(this.ExportSFChanged);
			// 
			// ExportFFNumeric
			// 
			this.ExportFFNumeric.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ExportFFNumeric.Location = new System.Drawing.Point(163, 62);
			this.ExportFFNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ExportFFNumeric.Name = "ExportFFNumeric";
			this.ExportFFNumeric.Size = new System.Drawing.Size(110, 23);
			this.ExportFFNumeric.TabIndex = 6;
			this.ExportFFNumeric.ValueChanged += new System.EventHandler(this.ExportFFChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(47, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 30);
			this.label1.TabIndex = 7;
			this.label1.Text = "Import Fuel Comsumption";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(163, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 30);
			this.label4.TabIndex = 8;
			this.label4.Text = "Export Fuel Consumption";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// IgnoreImport
			// 
			this.IgnoreImport.AutoSize = true;
			this.IgnoreImport.Location = new System.Drawing.Point(47, 91);
			this.IgnoreImport.Name = "IgnoreImport";
			this.IgnoreImport.Size = new System.Drawing.Size(60, 19);
			this.IgnoreImport.TabIndex = 9;
			this.IgnoreImport.Text = "Ignore";
			this.IgnoreImport.UseVisualStyleBackColor = true;
			// 
			// IgnoreExport
			// 
			this.IgnoreExport.AutoSize = true;
			this.IgnoreExport.Location = new System.Drawing.Point(163, 91);
			this.IgnoreExport.Name = "IgnoreExport";
			this.IgnoreExport.Size = new System.Drawing.Size(60, 19);
			this.IgnoreExport.TabIndex = 10;
			this.IgnoreExport.Text = "Ignore";
			this.IgnoreExport.UseVisualStyleBackColor = true;
			// 
			// ShippingLabel
			// 
			this.ShippingLabel.AutoSize = true;
			this.ShippingLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ShippingLabel.Location = new System.Drawing.Point(5, 5);
			this.ShippingLabel.Name = "ShippingLabel";
			this.ShippingLabel.Size = new System.Drawing.Size(115, 19);
			this.ShippingLabel.TabIndex = 0;
			this.ShippingLabel.Text = "Shipping Settings";
			// 
			// ViewTab
			// 
			this.ViewTab.AutoScroll = true;
			this.ViewTab.Location = new System.Drawing.Point(4, 24);
			this.ViewTab.Name = "ViewTab";
			this.ViewTab.Padding = new System.Windows.Forms.Padding(3);
			this.ViewTab.Size = new System.Drawing.Size(316, 642);
			this.ViewTab.TabIndex = 1;
			this.ViewTab.Text = "View";
			this.ViewTab.UseVisualStyleBackColor = true;
			// 
			// MainContainer
			// 
			this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainContainer.Location = new System.Drawing.Point(0, 0);
			this.MainContainer.Name = "MainContainer";
			// 
			// MainContainer.Panel1
			// 
			this.MainContainer.Panel1.Controls.Add(this.DataGrid);
			// 
			// MainContainer.Panel2
			// 
			this.MainContainer.Panel2.Controls.Add(this.SettingsToggleButton);
			this.MainContainer.Panel2.Controls.Add(this.Settings);
			this.MainContainer.Size = new System.Drawing.Size(1040, 670);
			this.MainContainer.SplitterDistance = 712;
			this.MainContainer.TabIndex = 2;
			// 
			// SettingsToggleButton
			// 
			this.SettingsToggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SettingsToggleButton.Image = global::ProsperousAssistant.Properties.Resources.Settings_12x_16x;
			this.SettingsToggleButton.Location = new System.Drawing.Point(301, 2);
			this.SettingsToggleButton.Name = "SettingsToggleButton";
			this.SettingsToggleButton.Size = new System.Drawing.Size(20, 20);
			this.SettingsToggleButton.TabIndex = 0;
			this.SettingsToggleButton.UseVisualStyleBackColor = true;
			this.SettingsToggleButton.Click += new System.EventHandler(this.ToggleSettingsPanel);
			// 
			// ProfitEstimatorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.MainContainer);
			this.Name = "ProfitEstimatorView";
			this.Size = new System.Drawing.Size(1040, 670);
			this.Load += new System.EventHandler(this.ProfitEstimatorView_Load);
			this.SizeChanged += new System.EventHandler(this.ProfitEstimatorView_SizeChanged);
			this.VisibleChanged += new System.EventHandler(this.ProfitEstimatorView_VisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
			this.Settings.ResumeLayout(false);
			this.CostsTab.ResumeLayout(false);
			this.CostsTab.PerformLayout();
			this.CostsFlowPanel.ResumeLayout(false);
			this.CostsFlowPanel.PerformLayout();
			this.CostsHeaderPanel.ResumeLayout(false);
			this.CostsHeaderPanel.PerformLayout();
			this.PriceModesPanel.ResumeLayout(false);
			this.PriceModesPanel.PerformLayout();
			this.ExpertisePanel.ResumeLayout(false);
			this.ExpertisePanel.PerformLayout();
			this.ExpertiseAndFeesTable.ResumeLayout(false);
			this.ExpertiseAndFeesTable.PerformLayout();
			this.PopulationsPanel.ResumeLayout(false);
			this.PopulationsPanel.PerformLayout();
			this.ShippingSettingsPanel.ResumeLayout(false);
			this.ShippingSettingsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FillMarginSelector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxVolumeSelector)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxMassSelector)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImportSFNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImportFFNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ExportSFNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ExportFFNumeric)).EndInit();
			this.MainContainer.Panel1.ResumeLayout(false);
			this.MainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
			this.MainContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView DataGrid;
		private System.Windows.Forms.TabControl Settings;
		private System.Windows.Forms.TabPage CostsTab;
		private System.Windows.Forms.TabPage ViewTab;
		private System.Windows.Forms.Button SavePresetButton;
		private System.Windows.Forms.Button LoadPresetButton;
		private System.Windows.Forms.Label TimeScaleLabel;
		private System.Windows.Forms.ComboBox TimeScaleSelector;
		private System.Windows.Forms.ComboBox FuelModeSelector;
		private System.Windows.Forms.Label FuelModeLabel;
		private System.Windows.Forms.ComboBox ConsumablesModeSelector;
		private System.Windows.Forms.Label ConsumablesModeLabel;
		private System.Windows.Forms.ComboBox SellModeSelector;
		private System.Windows.Forms.Label SellModeLabel;
		private System.Windows.Forms.ComboBox BuyModeSelector;
		private System.Windows.Forms.Label BuyModeLabel;
		private System.Windows.Forms.Label PriceModesLabel;
		private System.Windows.Forms.Label FieldHeading;
		private System.Windows.Forms.Label ExpertsHeading;
		private System.Windows.Forms.Label ProdFeeHeading;
		private System.Windows.Forms.Label ExpertiseAndFeesLabel;
		private System.Windows.Forms.Panel PriceModesPanel;
		private System.Windows.Forms.FlowLayoutPanel CostsFlowPanel;
		private System.Windows.Forms.Panel CostsHeaderPanel;
		private System.Windows.Forms.Panel ExpertisePanel;
		private System.Windows.Forms.Panel PopulationsPanel;
		private System.Windows.Forms.Label PopulationsAndConsumablesLabel;
		private System.Windows.Forms.FlowLayoutPanel PopulationsFlowPanel;
		private System.Windows.Forms.Button PopulationsVisibilityToggle;
		private System.Windows.Forms.SplitContainer MainContainer;
		private System.Windows.Forms.Button SettingsToggleButton;
		private System.Windows.Forms.Panel ShippingSettingsPanel;
		private System.Windows.Forms.Label ShippingLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown ImportSFNumeric;
		private System.Windows.Forms.NumericUpDown ImportFFNumeric;
		private System.Windows.Forms.NumericUpDown ExportSFNumeric;
		private System.Windows.Forms.NumericUpDown ExportFFNumeric;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox IgnoreImport;
		private System.Windows.Forms.CheckBox IgnoreExport;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox ShipBoundingSelector;
		private System.Windows.Forms.NumericUpDown MaxMassSelector;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown MaxVolumeSelector;
		private System.Windows.Forms.Label label8;
		private ProsperousAssistant.Util.PercentageUpDown FillMarginSelector;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox ExchangeSelector;
		private System.Windows.Forms.Label IgnoreCommoditiesLabel;
		private System.Windows.Forms.Button EditIgnoreButton;
		private System.Windows.Forms.TableLayoutPanel ExpertiseAndFeesTable;
	}
}
