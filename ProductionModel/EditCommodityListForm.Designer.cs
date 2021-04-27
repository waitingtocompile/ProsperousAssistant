
namespace ProsperousAssistant.ProductionModel
{
	partial class EditCommodityListForm
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCommodityListForm));
			this.SelectedCommodities = new System.Windows.Forms.ListView();
			this.UnselectedCommodities = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SelectedSortModeSelector = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.UnselectedSortModeSelector = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SelectedCommodities
			// 
			this.SelectedCommodities.HideSelection = false;
			this.SelectedCommodities.Location = new System.Drawing.Point(13, 68);
			this.SelectedCommodities.Name = "SelectedCommodities";
			this.SelectedCommodities.Size = new System.Drawing.Size(300, 400);
			this.SelectedCommodities.TabIndex = 0;
			this.SelectedCommodities.UseCompatibleStateImageBehavior = false;
			this.SelectedCommodities.View = System.Windows.Forms.View.Tile;
			this.SelectedCommodities.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SelectedCommodities_MouseDoubleClick);
			// 
			// UnselectedCommodities
			// 
			this.UnselectedCommodities.HideSelection = false;
			this.UnselectedCommodities.Location = new System.Drawing.Point(345, 68);
			this.UnselectedCommodities.Name = "UnselectedCommodities";
			this.UnselectedCommodities.Size = new System.Drawing.Size(300, 400);
			this.UnselectedCommodities.TabIndex = 2;
			this.UnselectedCommodities.UseCompatibleStateImageBehavior = false;
			this.UnselectedCommodities.View = System.Windows.Forms.View.Tile;
			this.UnselectedCommodities.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UnselectedCommodities_MouseDoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 19);
			this.label1.TabIndex = 4;
			this.label1.Text = "Selected Commodities";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(345, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(161, 19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Unselected Commodities";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 15);
			this.label3.TabIndex = 6;
			this.label3.Text = "Sort By";
			// 
			// SelectedSortModeSelector
			// 
			this.SelectedSortModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SelectedSortModeSelector.FormattingEnabled = true;
			this.SelectedSortModeSelector.Location = new System.Drawing.Point(63, 39);
			this.SelectedSortModeSelector.Name = "SelectedSortModeSelector";
			this.SelectedSortModeSelector.Size = new System.Drawing.Size(121, 23);
			this.SelectedSortModeSelector.TabIndex = 7;
			this.SelectedSortModeSelector.SelectedIndexChanged += new System.EventHandler(this.SelectedSortModeChanged);
			// 
			// button1
			// 
			this.button1.Image = global::ProsperousAssistant.Properties.Resources.LeftArrowAsterisk_16x;
			this.button1.Location = new System.Drawing.Point(319, 129);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 20);
			this.button1.TabIndex = 8;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AddSelectedItems);
			// 
			// button2
			// 
			this.button2.Image = global::ProsperousAssistant.Properties.Resources.RightArrowAsterisk_16x;
			this.button2.Location = new System.Drawing.Point(319, 103);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(20, 20);
			this.button2.TabIndex = 9;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.RemoveSelectedItems);
			// 
			// UnselectedSortModeSelector
			// 
			this.UnselectedSortModeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UnselectedSortModeSelector.FormattingEnabled = true;
			this.UnselectedSortModeSelector.Location = new System.Drawing.Point(395, 39);
			this.UnselectedSortModeSelector.Name = "UnselectedSortModeSelector";
			this.UnselectedSortModeSelector.Size = new System.Drawing.Size(121, 23);
			this.UnselectedSortModeSelector.TabIndex = 11;
			this.UnselectedSortModeSelector.SelectedIndexChanged += new System.EventHandler(this.UnselectedSortModeChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(345, 42);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Sort By";
			// 
			// EditCommodityListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(658, 480);
			this.Controls.Add(this.UnselectedSortModeSelector);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.SelectedSortModeSelector);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.UnselectedCommodities);
			this.Controls.Add(this.SelectedCommodities);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "EditCommodityListForm";
			this.Text = "Edit Selected Commodities";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView SelectedCommodities;
		private System.Windows.Forms.ListView UnselectedCommodities;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox SelectedSortModeSelector;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox UnselectedSortModeSelector;
		private System.Windows.Forms.Label label4;
	}
}