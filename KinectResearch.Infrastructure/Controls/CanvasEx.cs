using System.Windows;
using System.Windows.Controls;

namespace KinectResearch.Infrastructure.Controls
{
	public class CanvasEx : Canvas
	{
		public static readonly DependencyProperty PanelWidthProperty =
			DependencyProperty.Register("PanelWidth", typeof (double), typeof (CanvasEx), new PropertyMetadata(0.0));

		public static readonly DependencyProperty PanelHeightProperty =
			DependencyProperty.Register("PanelHeight", typeof (double), typeof (CanvasEx), new PropertyMetadata(0.0));

		public CanvasEx()
		{
			SizeChanged += OnCanvasSizeChanged;
		}

		public double PanelWidth
		{
			get { return (double) GetValue(PanelWidthProperty); }
			set { SetValue(PanelWidthProperty, value); }
		}

		public double PanelHeight
		{
			get { return (double) GetValue(PanelHeightProperty); }
			set { SetValue(PanelHeightProperty, value); }
		}

		private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
		{
			PanelWidth = ActualWidth;
			PanelHeight = ActualHeight;
		}
	}
}