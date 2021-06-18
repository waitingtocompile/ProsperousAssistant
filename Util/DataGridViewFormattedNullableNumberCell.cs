using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ProsperousAssistant.Util
{

	public class DataGridViewFormattedNullableNumberCell : DataGridViewTextBoxCell
	{
		//Oh god how did I do this to myself this is such a disgusting name


		public string FormatString = "N2";
		public string NullString = "--";
		public IFormatProvider FormatProvider = CultureInfo.InvariantCulture;
		public bool AlwaysPositive = false;

		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (value == null) return NullString;
			if (value is decimal decimalValue)
			{
				if (AlwaysPositive && decimalValue < 0) decimalValue *= -1;
				return string.Format(FormatProvider, "{0:" + FormatString + "}", decimalValue);
			}
			return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
		}
	}
}
