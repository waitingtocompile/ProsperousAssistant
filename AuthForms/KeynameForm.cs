using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant.AuthForms
{
	public partial class KeynameForm : Form
	{

		public KeynameForm()
		{
			InitializeComponent();
		}

		private void Ok(object sender, EventArgs e)
		{
			if(KeyField.Text.Length > 0) DialogResult = DialogResult.OK;
		}

		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
