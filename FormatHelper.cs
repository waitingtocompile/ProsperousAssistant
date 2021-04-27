using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProsperousAssistant
{
	public static class FormatHelper
	{
		public static readonly CultureInfo ACCOUNTING_CULTURE;


		static FormatHelper()
		{
			ACCOUNTING_CULTURE = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			ACCOUNTING_CULTURE.NumberFormat.NegativeSign = "()";
			ACCOUNTING_CULTURE.NumberFormat.CurrencySymbol = "";
		}

	}
}
