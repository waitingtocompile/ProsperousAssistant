using ProsperousAssistant.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public static class FontHelper
	{
		public static FontFamily DroidSans { get
			{
				if(_droidSans == null)
				{
					LoadFont(Resources.DroidSans);
					LoadFont(Resources.DroidSans_Bold);
					_droidSans = _privateFontCollection.Families.Where(family => family.Name.Equals("Droid Sans", StringComparison.OrdinalIgnoreCase)).Single();
				}
				return _droidSans;
			} }

		private static FontFamily _droidSans;


		private static PrivateFontCollection _privateFontCollection;
		private static void LoadFont(byte[] FontData)
		{
			if(_privateFontCollection == null)
			{
				_privateFontCollection = new PrivateFontCollection();
				Application.ApplicationExit += (object sender, EventArgs e) => _privateFontCollection.Dispose();
			}
			IntPtr fontPtr = Marshal.AllocCoTaskMem(FontData.Length);
			Marshal.Copy(FontData, 0, fontPtr, FontData.Length);
			_privateFontCollection.AddMemoryFont(fontPtr, FontData.Length);
			Marshal.FreeCoTaskMem(fontPtr);
		}
	}
}
