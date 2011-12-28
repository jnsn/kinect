using System;
using System.Globalization;
using System.Windows.Data;

namespace KinectResearch.Infrastructure.Converters
{
	public abstract class AbstractValueConverter : AbstractMarkupExtension, IValueConverter
	{
		#region IValueConverter Members

		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}