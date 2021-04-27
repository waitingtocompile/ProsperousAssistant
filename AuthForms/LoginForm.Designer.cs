namespace ProsperousAssistant.AuthForms
{
	partial class LoginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.InfoLabel = new System.Windows.Forms.Label();
			this.UserField = new System.Windows.Forms.TextBox();
			this.UserLabel = new System.Windows.Forms.Label();
			this.PassField = new System.Windows.Forms.TextBox();
			this.PassLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.QuitButton = new System.Windows.Forms.Button();
			this.DiscordLabel = new System.Windows.Forms.LinkLabel();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.APIKeysButton = new System.Windows.Forms.Button();
			this.APIInfoLabel = new System.Windows.Forms.Label();
			this.APIKeyField = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// InfoLabel
			// 
			this.InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InfoLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.InfoLabel.Location = new System.Drawing.Point(12, 9);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(431, 112);
			this.InfoLabel.TabIndex = 0;
			this.InfoLabel.Text = resources.GetString("InfoLabel.Text");
			// 
			// UserField
			// 
			this.UserField.Location = new System.Drawing.Point(78, 124);
			this.UserField.Name = "UserField";
			this.UserField.Size = new System.Drawing.Size(365, 23);
			this.UserField.TabIndex = 0;
			// 
			// UserLabel
			// 
			this.UserLabel.AutoSize = true;
			this.UserLabel.Location = new System.Drawing.Point(12, 127);
			this.UserLabel.Name = "UserLabel";
			this.UserLabel.Size = new System.Drawing.Size(60, 15);
			this.UserLabel.TabIndex = 2;
			this.UserLabel.Text = "Username";
			// 
			// PassField
			// 
			this.PassField.Location = new System.Drawing.Point(78, 153);
			this.PassField.Name = "PassField";
			this.PassField.Size = new System.Drawing.Size(365, 23);
			this.PassField.TabIndex = 1;
			// 
			// PassLabel
			// 
			this.PassLabel.AutoSize = true;
			this.PassLabel.Location = new System.Drawing.Point(12, 156);
			this.PassLabel.Name = "PassLabel";
			this.PassLabel.Size = new System.Drawing.Size(57, 15);
			this.PassLabel.TabIndex = 2;
			this.PassLabel.Text = "Password";
			// 
			// OkButton
			// 
			this.OkButton.AutoSize = true;
			this.OkButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.OkButton.Location = new System.Drawing.Point(287, 205);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 29);
			this.OkButton.TabIndex = 2;
			this.OkButton.Text = "Log In";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.TryLogin);
			// 
			// QuitButton
			// 
			this.QuitButton.AutoSize = true;
			this.QuitButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.QuitButton.Location = new System.Drawing.Point(368, 205);
			this.QuitButton.Name = "QuitButton";
			this.QuitButton.Size = new System.Drawing.Size(75, 29);
			this.QuitButton.TabIndex = 3;
			this.QuitButton.Text = "Quit";
			this.QuitButton.UseVisualStyleBackColor = true;
			this.QuitButton.Click += new System.EventHandler(this.Quit);
			// 
			// DiscordLabel
			// 
			this.DiscordLabel.AutoSize = true;
			this.DiscordLabel.Location = new System.Drawing.Point(12, 213);
			this.DiscordLabel.Name = "DiscordLabel";
			this.DiscordLabel.Size = new System.Drawing.Size(173, 15);
			this.DiscordLabel.TabIndex = 4;
			this.DiscordLabel.TabStop = true;
			this.DiscordLabel.Text = "PrUn Community Tools Discord";
			this.DiscordLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DiscordLabelClicked);
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = true;
			this.StatusLabel.ForeColor = System.Drawing.Color.Red;
			this.StatusLabel.Location = new System.Drawing.Point(78, 183);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(0, 15);
			this.StatusLabel.TabIndex = 5;
			// 
			// APIKeysButton
			// 
			this.APIKeysButton.AutoSize = true;
			this.APIKeysButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.APIKeysButton.Location = new System.Drawing.Point(309, 336);
			this.APIKeysButton.Name = "APIKeysButton";
			this.APIKeysButton.Size = new System.Drawing.Size(134, 29);
			this.APIKeysButton.TabIndex = 6;
			this.APIKeysButton.Text = "Login with API Key";
			this.APIKeysButton.UseVisualStyleBackColor = true;
			this.APIKeysButton.Click += new System.EventHandler(this.TryKey);
			// 
			// APIInfoLabel
			// 
			this.APIInfoLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.APIInfoLabel.Location = new System.Drawing.Point(12, 248);
			this.APIInfoLabel.Name = "APIInfoLabel";
			this.APIInfoLabel.Size = new System.Drawing.Size(431, 83);
			this.APIInfoLabel.TabIndex = 6;
			this.APIInfoLabel.Text = resources.GetString("APIInfoLabel.Text");
			// 
			// APIKeyField
			// 
			this.APIKeyField.Location = new System.Drawing.Point(12, 339);
			this.APIKeyField.Name = "APIKeyField";
			this.APIKeyField.Size = new System.Drawing.Size(291, 23);
			this.APIKeyField.TabIndex = 5;
			// 
			// LoginForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.QuitButton;
			this.ClientSize = new System.Drawing.Size(455, 377);
			this.Controls.Add(this.APIKeyField);
			this.Controls.Add(this.APIInfoLabel);
			this.Controls.Add(this.APIKeysButton);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DiscordLabel);
			this.Controls.Add(this.QuitButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.PassLabel);
			this.Controls.Add(this.PassField);
			this.Controls.Add(this.UserLabel);
			this.Controls.Add(this.UserField);
			this.Controls.Add(this.InfoLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.Text = "FIO Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.TextBox UserField;
		private System.Windows.Forms.Label UserLabel;
		private System.Windows.Forms.Label PassLabel;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button QuitButton;
		private System.Windows.Forms.LinkLabel DiscordLabel;
		public System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.Button APIKeysButton;
		private System.Windows.Forms.Label APIInfoLabel;
		private System.Windows.Forms.TextBox APIKeyField;
		public System.Windows.Forms.TextBox PassField;
	}
}