namespace ProsperousAssistant.AuthForms
{
	partial class ConfirmPasswordForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmPasswordForm));
			this.InfoLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.PasswordField = new System.Windows.Forms.TextBox();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// InfoLabel
			// 
			this.InfoLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.InfoLabel.Location = new System.Drawing.Point(12, 13);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(426, 72);
			this.InfoLabel.TabIndex = 0;
			this.InfoLabel.Text = "To manage your API keys, you must confirm the password for {NAME}.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 91);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Password";
			// 
			// PasswordField
			// 
			this.PasswordField.Location = new System.Drawing.Point(75, 88);
			this.PasswordField.Name = "PasswordField";
			this.PasswordField.Size = new System.Drawing.Size(363, 23);
			this.PasswordField.TabIndex = 0;
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = true;
			this.StatusLabel.ForeColor = System.Drawing.Color.Red;
			this.StatusLabel.Location = new System.Drawing.Point(12, 134);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(0, 15);
			this.StatusLabel.TabIndex = 3;
			// 
			// OkButton
			// 
			this.OkButton.AutoSize = true;
			this.OkButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.OkButton.Location = new System.Drawing.Point(282, 117);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 29);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.CheckPassword);
			// 
			// CancelButton
			// 
			this.CancelButton.AutoSize = true;
			this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CancelButton.Location = new System.Drawing.Point(363, 117);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(75, 29);
			this.CancelButton.TabIndex = 2;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.Cancel);
			// 
			// ConfirmPasswordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(443, 158);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.PasswordField);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.InfoLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfirmPasswordForm";
			this.Text = "Confirm Password";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.Button OkButton;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
		private System.Windows.Forms.Button CancelButton;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
		public System.Windows.Forms.TextBox PasswordField;
	}
}