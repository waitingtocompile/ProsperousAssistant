using System;

namespace ProsperousAssistant.AuthForms
{
	partial class KeynameForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeynameForm));
			this.label1 = new System.Windows.Forms.Label();
			this.CancelButton = new System.Windows.Forms.Button();
			this.OkButton = new System.Windows.Forms.Button();
			this.KeyField = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(286, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please enter the name for the new API key";
			// 
			// CancelButton
			// 
			this.CancelButton.AutoSize = true;
			this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.CancelButton.Location = new System.Drawing.Point(298, 66);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(75, 29);
			this.CancelButton.TabIndex = 2;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.Cancel);
			// 
			// OkButton
			// 
			this.OkButton.AutoSize = true;
			this.OkButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.OkButton.Location = new System.Drawing.Point(217, 66);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 29);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.Ok);
			// 
			// KeyField
			// 
			this.KeyField.Location = new System.Drawing.Point(12, 37);
			this.KeyField.Name = "KeyField";
			this.KeyField.Size = new System.Drawing.Size(361, 23);
			this.KeyField.TabIndex = 0;
			// 
			// KeynameForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelButton;
			this.ClientSize = new System.Drawing.Size(385, 102);
			this.Controls.Add(this.KeyField);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "KeynameForm";
			this.Text = "Enter API Key Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
		private System.Windows.Forms.Button CancelButton;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
		private System.Windows.Forms.Button OkButton;
		public System.Windows.Forms.TextBox KeyField;
	}
}