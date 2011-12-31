using System.Windows;
using KinectResearch.Infrastructure.Interfaces;

namespace KinectResearch.Modules.Preview.Views
{
	/// <summary>
	/// Interaction logic for ColorPreviewView.xaml
	/// </summary>
	public partial class ColorPreviewView : IColorPreviewView
	{
		private readonly ColorPreviewViewModel _colorPreviewViewModel;

		public ColorPreviewView()
		{
			Loaded += OnControlLoaded;
			Unloaded += OnControlUnloaded;

			InitializeComponent();
		}

		public ColorPreviewView(ColorPreviewViewModel colorPreviewViewModel)
			: this()
		{
			_colorPreviewViewModel = colorPreviewViewModel;

			DataContext = _colorPreviewViewModel;
		}

		private void OnControlLoaded(object sender, RoutedEventArgs e)
		{
			_colorPreviewViewModel.Initialize();
		}

		private void OnControlUnloaded(object sender, RoutedEventArgs e)
		{
			_colorPreviewViewModel.Uninitialize();
		}
	}
}