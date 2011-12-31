using System.Windows;
using System.Windows.Media;

namespace KinectResearch.Infrastructure
{
	public static class VisualTreeHelperExtensions
	{
		public static T FindAncestor<T>(this DependencyObject dependencyObject)
			where T : class
		{
			var target = dependencyObject;
			do
			{
				target = VisualTreeHelper.GetParent(target);
			} while (target != null && !(target is T));

			return target as T;
		}
	}
}