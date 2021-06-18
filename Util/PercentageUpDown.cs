using System.Windows.Forms;

namespace ProsperousAssistant.Util
{
	public class PercentageUpDown : NumericUpDown
	{
		protected override void UpdateEditText()
		{
			Text = Value + "%";
		}



	}
}
