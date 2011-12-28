using System;
using System.Windows.Markup;

namespace KinectResearch.Infrastructure.Converters
{
	public abstract class AbstractMarkupExtension : MarkupExtension
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}