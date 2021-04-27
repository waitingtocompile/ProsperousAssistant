
namespace ProsperousAssistant
{
	partial class SplashForm
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.AssistantSetupWorker = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::ProsperousAssistant.Properties.Resources.splash;
			this.pictureBox1.InitialImage = global::ProsperousAssistant.Properties.Resources.splash;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.MaximumSize = new System.Drawing.Size(1047, 448);
			this.pictureBox1.MinimumSize = new System.Drawing.Size(1047, 448);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1047, 448);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 491);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(1044, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = true;
			this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.StatusLabel.ForeColor = System.Drawing.SystemColors.Control;
			this.StatusLabel.Location = new System.Drawing.Point(12, 467);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(90, 21);
			this.StatusLabel.TabIndex = 2;
			this.StatusLabel.Text = "Initializing...";
			// 
			// AssistantSetupWorker
			// 
			this.AssistantSetupWorker.WorkerReportsProgress = true;
			this.AssistantSetupWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AssistantSetupWorker_DoWork);
			this.AssistantSetupWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AssistantSetupWorker_ProgressChanged);
			this.AssistantSetupWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AssistantSetupWorker_RunWorkerCompleted);
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(1166, 659);
			this.ControlBox = false;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashForm";
			this.Load += new System.EventHandler(this.SplashForm_Load);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label StatusLabel;
		private System.ComponentModel.BackgroundWorker AssistantSetupWorker;
	}
}