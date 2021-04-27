using System;
using System.IO;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new SplashForm());
		}
	}
}
