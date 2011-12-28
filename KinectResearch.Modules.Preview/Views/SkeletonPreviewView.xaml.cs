using System.Windows;

namespace KinectResearch.Modules.Preview.Views
{
	/// <summary>
	/// Interaction logic for SkeletonPreviewView.xaml
	/// </summary>
	public partial class SkeletonPreviewView
	{
		private readonly SkeletonPreviewViewModel _skeletonPreviewViewModel;

		public SkeletonPreviewView()
		{
			Loaded += OnControlLoaded;
			Unloaded += OnControlUnloaded;

			InitializeComponent();
		}

		public SkeletonPreviewView(SkeletonPreviewViewModel skeletonPreviewViewModel)
			: this()
		{
			_skeletonPreviewViewModel = skeletonPreviewViewModel;

			DataContext = _skeletonPreviewViewModel;
		}

		private void OnControlLoaded(object sender, RoutedEventArgs e)
		{
			_skeletonPreviewViewModel.Initialize();
		}

		private void OnControlUnloaded(object sender, RoutedEventArgs e)
		{
			_skeletonPreviewViewModel.Uninitialize();
		}
	}
}