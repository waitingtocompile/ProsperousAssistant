using System;
using System.Net;
using System.Windows.Forms;
using FIOSharp;

namespace ProsperousAssistant.AuthForms
{
	public partial class LoginForm : Form
	{
		private FnarOracleDataSource oracle;

		public LoginForm(FnarOracleDataSource oracle)
		{
			this.oracle = oracle;
			InitializeComponent();
			PassField.Text = "";
			PassField.UseSystemPasswordChar = true;

		}

		private void DiscordLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DiscordLabel.LinkVisited = true;
			WebHelper.OpenBrowser("https://discord.gg/2MDR5DYSfY");
		}

		private void TryLogin(object sender, EventArgs e)
		{
			HttpStatusCode result = oracle.LoginAs(UserField.Text, PassField.Text);
			switch (result)
			{
				case HttpStatusCode.OK:
					DialogResult = DialogResult.OK;
					break;
				case HttpStatusCode.Unauthorized:
					StatusLabel.Text = "Invalid username or password, check your credentials";
					break;
				default:
					StatusLabel.Text = "Something went wrong trying to log in. The server may be inacessible or offline.";
					break;
			}
		}

		private void TryKey(object sender, EventArgs e)
		{
			switch (oracle.LoginWithAPIKey(APIKeyField.Text))
			{
				case HttpStatusCode.OK:
					Settings.APIKey = APIKeyField.Text;
					DialogResult = DialogResult.OK;
					break;
				case HttpStatusCode.Unauthorized:
					StatusLabel.Text = "Invalid API key, make sure you copied the whole thing";
					break;
				default:
					StatusLabel.Text = "Something went wrong trying to validate the API key. The server may be inacessible or offline.";
					break;
			}
		}

		private void Quit(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
