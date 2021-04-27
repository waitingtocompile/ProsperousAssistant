
namespace ProsperousAssistant
{
	partial class SaveOrLoadPresetDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveOrLoadPresetDialog));
			this.FileListBox = new System.Windows.Forms.ListBox();
			this.FileNameBox = new System.Windows.Forms.TextBox();
			this.SaveLoadButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// FileListBox
			// 
			this.FileListBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FileListBox.FormattingEnabled = true;
			this.FileListBox.ItemHeight = 17;
			this.FileListBox.Items.AddRange(new object[] {
            "item1",
            "item2",
            "item3"});
			this.FileListBox.Location = new System.Drawing.Point(6, 6);
			this.FileListBox.Name = "FileListBox";
			this.FileListBox.ScrollAlwaysVisible = true;
			this.FileListBox.Size = new System.Drawing.Size(336, 378);
			this.FileListBox.TabIndex = 0;
			this.FileListBox.SelectedValueChanged += new System.EventHandler(this.OnSelectionChanged);
			// 
			// FileNameBox
			// 
			this.FileNameBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FileNameBox.Location = new System.Drawing.Point(6, 391);
			this.FileNameBox.Name = "FileNameBox";
			this.FileNameBox.Size = new System.Drawing.Size(255, 25);
			this.FileNameBox.TabIndex = 1;
			this.FileNameBox.TextChanged += new System.EventHandler(this.FileNameChanged);
			// 
			// SaveLoadButton
			// 
			this.SaveLoadButton.Enabled = false;
			this.SaveLoadButton.Location = new System.Drawing.Point(267, 390);
			this.SaveLoadButton.Name = "SaveLoadButton";
			this.SaveLoadButton.Padding = new System.Windows.Forms.Padding(3);
			this.SaveLoadButton.Size = new System.Drawing.Size(75, 27);
			this.SaveLoadButton.TabIndex = 2;
			this.SaveLoadButton.Text = "button1";
			this.SaveLoadButton.UseVisualStyleBackColor = true;
			this.SaveLoadButton.Click += new System.EventHandler(this.SaveOrLoadFile);
			// 
			// SaveOrLoadPresetDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(360, 442);
			this.Controls.Add(this.SaveLoadButton);
			this.Controls.Add(this.FileNameBox);
			this.Controls.Add(this.FileListBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveOrLoadPresetDialog";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Text = "LoadPresetDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox FileListBox;
		private System.Windows.Forms.TextBox FileNameBox;
		private System.Windows.Forms.Button SaveLoadButton;
	}
}