using System.Globalization;

namespace ProsperousAssistant.Util
{
	public static class FormatCultures
	{
		public static readonly CultureInfo ACCOUNTING_CULTURE;


		static FormatCultures()
		{
			ACCOUNTING_CULTURE = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			ACCOUNTING_CULTURE.NumberFormat.NegativeSign = "()";
			ACCOUNTING_CULTURE.NumberFormat.CurrencySymbol = "";
		}

	}
}
