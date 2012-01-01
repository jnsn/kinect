using System.Windows;
using KinectResearch.Infrastructure.Interfaces;

namespace KinectResearch.Modules.Details.Views
{
	/// <summary>
	/// Interaction logic for DetailsView.xaml
	/// </summary>
	public partial class DetailsView : IDetailsView
	{
		private readonly IDetailsViewModel _detailsViewModel;

		public DetailsView()
		{
			Loaded += OnControlLoaded;
			Unloaded += OnControlUnloaded;

			InitializeComponent();
		}

		public DetailsView(IDetailsViewModel detailsViewModel)
			: this()
		{
			_detailsViewModel = detailsViewModel;

			DataContext = _detailsViewModel;
		}

		private void OnControlLoaded(object sender, RoutedEventArgs e)
		{
			_detailsViewModel.Initialize();
		}

		private void OnControlUnloaded(object sender, RoutedEventArgs e)
		{
			_detailsViewModel.Uninitialize();
		}
	}
}