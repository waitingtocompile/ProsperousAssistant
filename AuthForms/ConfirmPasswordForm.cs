using FIOSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant.AuthForms
{
	public partial class ConfirmPasswordForm : Form
	{
		FnarOracleDataSource oracle;

		public ConfirmPasswordForm(FnarOracleDataSource oracle)
		{
			this.oracle = oracle;
			InitializeComponent();
			PasswordField.Text = "";
			PasswordField.UseSystemPasswordChar = true;
		}



		private void CheckPassword(object sender, EventArgs e)
		{
			switch (oracle.LoginAs(oracle.AuthoriedAs, PasswordField.Text, true))
			{
				case HttpStatusCode.OK:
					DialogResult = DialogResult.OK;
					break;
				case HttpStatusCode.Unauthorized:
					StatusLabel.Text = "Invalid password, check you entered it correctly and that you're logged into the account you though you were";
					break;
				default:
					StatusLabel.Text = "Something went wrong trying to log in. The server may be inacessible or offline.";
					break;
			}
		}

		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void OnLoad(object sender, EventArgs e)
		{
			InfoLabel.Text = InfoLabel.Text.Replace("{NAME}", oracle.AuthoriedAs.ToLower());
		}
	}
}
