using System;
using System.Globalization;
using System.Windows.Data;

namespace KinectResearch.Infrastructure.Converters
{
	[ValueConversion(typeof (bool), typeof (double))]
	public class BoolToScaleXConverter : AbstractValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var condition = System.Convert.ToBoolean(value);
			return condition ? -1 : 1;
		}
	}
}