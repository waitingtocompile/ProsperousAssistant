namespace ProsperousAssistant.AuthForms
{
	partial class CreateAPIKeyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAPIKeyForm));
			this.InfoLabel = new System.Windows.Forms.Label();
			this.YesButton = new System.Windows.Forms.Button();
			this.NoButton = new System.Windows.Forms.Button();
			this.NeverButton = new System.Windows.Forms.Button();
			this.ManageButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// InfoLabel
			// 
			this.InfoLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.InfoLabel.Location = new System.Drawing.Point(13, 13);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(458, 67);
			this.InfoLabel.TabIndex = 0;
			this.InfoLabel.Text = "Would you like to generate an API key?  API keys are a secure way to access FIO w" +
    "ithout needing to enter your username and password every time you launch the pro" +
    "gram.";
			// 
			// YesButton
			// 
			this.YesButton.AutoSize = true;
			this.YesButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.YesButton.Location = new System.Drawing.Point(13, 83);
			this.YesButton.Name = "YesButton";
			this.YesButton.Size = new System.Drawing.Size(75, 29);
			this.YesButton.TabIndex = 0;
			this.YesButton.Text = "Yes";
			this.YesButton.UseVisualStyleBackColor = true;
			this.YesButton.Click += new System.EventHandler(this.CreateKey);
			// 
			// NoButton
			// 
			this.NoButton.AutoSize = true;
			this.NoButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NoButton.Location = new System.Drawing.Point(226, 83);
			this.NoButton.Name = "NoButton";
			this.NoButton.Size = new System.Drawing.Size(75, 29);
			this.NoButton.TabIndex = 2;
			this.NoButton.Text = "No";
			this.NoButton.UseVisualStyleBackColor = true;
			this.NoButton.Click += new System.EventHandler(this.Cancel);
			// 
			// NeverButton
			// 
			this.NeverButton.AutoSize = true;
			this.NeverButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.NeverButton.Location = new System.Drawing.Point(307, 83);
			this.NeverButton.Name = "NeverButton";
			this.NeverButton.Size = new System.Drawing.Size(164, 29);
			this.NeverButton.TabIndex = 3;
			this.NeverButton.Text = "No, and don\'t ask again";
			this.NeverButton.UseVisualStyleBackColor = true;
			this.NeverButton.Click += new System.EventHandler(this.CancelNeverAsk);
			// 
			// ManageButton
			// 
			this.ManageButton.AutoSize = true;
			this.ManageButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ManageButton.Location = new System.Drawing.Point(94, 83);
			this.ManageButton.Name = "ManageButton";
			this.ManageButton.Size = new System.Drawing.Size(126, 29);
			this.ManageButton.TabIndex = 1;
			this.ManageButton.Text = "Manage API Keys";
			this.ManageButton.UseVisualStyleBackColor = true;
			this.ManageButton.Click += new System.EventHandler(this.ManageKeys);
			// 
			// CreateAPIKeyForm
			// 
			this.AcceptButton = this.YesButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.NoButton;
			this.ClientSize = new System.Drawing.Size(483, 119);
			this.Controls.Add(this.ManageButton);
			this.Controls.Add(this.NeverButton);
			this.Controls.Add(this.NoButton);
			this.Controls.Add(this.YesButton);
			this.Controls.Add(this.InfoLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreateAPIKeyForm";
			this.Text = "Generate API Key";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.Button YesButton;
		private System.Windows.Forms.Button NoButton;
		private System.Windows.Forms.Button NeverButton;
		private System.Windows.Forms.Button ManageButton;
	}
}