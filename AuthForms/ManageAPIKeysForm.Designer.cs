namespace ProsperousAssistant.AuthForms
{
	partial class ManageAPIKeysForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAPIKeysForm));
			this.DeleteButton = new System.Windows.Forms.Button();
			this.CreateButton = new System.Windows.Forms.Button();
			this.CopyButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// DeleteButton
			// 
			this.DeleteButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DeleteButton.Location = new System.Drawing.Point(433, 346);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(85, 29);
			this.DeleteButton.TabIndex = 1;
			this.DeleteButton.Text = "Delete Key";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteKey);
			// 
			// CreateButton
			// 
			this.CreateButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CreateButton.Location = new System.Drawing.Point(151, 346);
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.Size = new System.Drawing.Size(115, 29);
			this.CreateButton.TabIndex = 1;
			this.CreateButton.Text = "Create New Key";
			this.CreateButton.UseVisualStyleBackColor = true;
			this.CreateButton.Click += new System.EventHandler(this.CreateKey);
			// 
			// CopyButton
			// 
			this.CopyButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CopyButton.Location = new System.Drawing.Point(272, 346);
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(155, 29);
			this.CopyButton.TabIndex = 1;
			this.CopyButton.Text = "Copy Key to Clipboard";
			this.CopyButton.UseVisualStyleBackColor = true;
			this.CopyButton.Click += new System.EventHandler(this.CopyKey);
			// 
			// ManageAPIKeysForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 387);
			this.Controls.Add(this.CopyButton);
			this.Controls.Add(this.CreateButton);
			this.Controls.Add(this.DeleteButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ManageAPIKeysForm";
			this.Text = "Manage API Keys";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Click += new System.EventHandler(this.DeleteKey);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button CreateButton;
		private System.Windows.Forms.Button CopyButton;
	}
}