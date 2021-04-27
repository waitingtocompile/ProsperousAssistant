using FIOSharp;
using System;
using System.Windows.Forms;

namespace ProsperousAssistant.AuthForms
{
	public partial class CreateAPIKeyForm : Form
	{
		private FnarOracleDataSource oracle;
		private string storedPassword;

		public CreateAPIKeyForm(FnarOracleDataSource oracle, string rememberedPassword)
		{
			this.oracle = oracle;
			storedPassword = rememberedPassword;
			InitializeComponent();
		}



		private void CreateKey(object sender, EventArgs e)
		{
			//actually create the key
			try
			{
				string key = oracle.CreateAPIKey("Prosperous Assistant", storedPassword);
				oracle.LoginWithAPIKey(key);
				Settings.APIKey = key;
				DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ManageKeys(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			ManageAPIKeysForm keyManagerForm = new ManageAPIKeysForm(oracle, storedPassword);
			keyManagerForm.ShowDialog();
		}

		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void CancelNeverAsk(object sender, EventArgs e)
		{
			Settings.NeverPromptCreateKey = true;
			Cancel(sender, e);
		}
	}
}
