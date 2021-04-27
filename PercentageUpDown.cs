using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public class PercentageUpDown : NumericUpDown
	{
		protected override void UpdateEditText()
		{
			Text = Value + "%";
		}



	}
}
