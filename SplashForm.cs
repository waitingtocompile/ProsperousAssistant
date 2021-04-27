﻿using FIOSharp;
using ProsperousAssistant.AuthForms;
using ProsperousAssistant.ProductionModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public partial class SplashForm : Form
	{
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();



		private Form MainForm;

		public SplashForm()
		{
			InitializeComponent();
			
			foreach(object c in Controls)
			{
				if(c is Control control)
				{
					control.MouseMove += OnMouseMove;
				}
			}
		}


		private void SplashForm_Load(object sender, EventArgs e)
		{
			AssistantSetupWorker.RunWorkerAsync();
		}

		private void AssistantSetupWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			AssistantSetupWorker.ReportProgress(10, "Reading Settings File");
			string storagePath = Settings.StoragePath;
			Directory.CreateDirectory(storagePath);
			FnarOracleDataSource oracleClient = new FnarOracleDataSource();

			if (Settings.APIKey.Length > 0)
			{
				AssistantSetupWorker.ReportProgress(20, "Logging in to FIO API");
				oracleClient.LoginWithAPIKey(Settings.APIKey);
			}

			if (oracleClient.AuthoriedAs == null)
			{
				//we aren't logged in, launch the login form
				AssistantSetupWorker.ReportProgress(20, "Logging in to FIO API");

				LoginForm loginWindow = new LoginForm(oracleClient);
				if (Settings.APIKey.Length > 0)
				{
					Settings.APIKey = "";
					loginWindow.StatusLabel.Text = "The stored API key was invalid, please log in";
				}
				if (loginWindow.ShowDialog() != DialogResult.OK)
				{
					Application.Exit();
				}
				AssistantSetupWorker.ReportProgress(60, "Logged in");

				if (oracleClient.AuthKeyExpiry != null && !Settings.NeverPromptCreateKey && Settings.APIKey.Length == 0)
				{
					AssistantSetupWorker.ReportProgress(20, "Logged in to FIO API");
					//we aren't using an API key and haven't been instructed to never prompt for one
					CreateAPIKeyForm createAPIKeyForm = new CreateAPIKeyForm(oracleClient, loginWindow.PassField.Text);
					createAPIKeyForm.ShowDialog();
					//whatever the result, we move on to actually launching the program
				}
			}

			AssistantSetupWorker.ReportProgress(60, "Preparing data helper");
			CachedDataHelper dataHelper = new CachedDataHelper(oracleClient, $"{storagePath}\\fio_cache");


			AssistantSetupWorker.ReportProgress(65, "Loading display settings");
			ColorGroup.LoadActiveColorsFromFile();



			AssistantSetupWorker.ReportProgress(60, "Preparing main window");


			//todo: replace with our actual main form
			MainForm = new TestForm();
			var control = new ProfitEstimatorView(dataHelper);
			MainForm.Controls.Add(control);
			control.Dock = DockStyle.Fill;

			AssistantSetupWorker.ReportProgress(100, "Launching main window");

		}

		private void AssistantSetupWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
			if(e.UserState is string newLabel)
			{
				StatusLabel.Text = newLabel;
			}

		}

		private void AssistantSetupWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//bind our "main" form to also close this when it's done
			MainForm.StartPosition = FormStartPosition.CenterParent;
			MainForm.FormClosed += (sender, e) => Close();
			Hide();
			MainForm.Show();
			MainForm.Focus();
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}
	}
}
