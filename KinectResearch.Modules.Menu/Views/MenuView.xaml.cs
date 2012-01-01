using System.Windows;
using KinectResearch.Infrastructure.Interfaces;

namespace KinectResearch.Modules.Menu.Views
{
	/// <summary>
	/// Interaction logic for MenuView.xaml
	/// </summary>
	public partial class MenuView : IMenuView
	{
		private readonly IMenuViewModel _menuViewModel;

		public MenuView()
		{
			Loaded += OnControlLoaded;
			Unloaded += OnControlUnloaded;

			InitializeComponent();
		}

		public MenuView(IMenuViewModel menuViewModel)
			: this()
		{
			_menuViewModel = menuViewModel;

			DataContext = _menuViewModel;
		}

		private void OnControlLoaded(object sender, RoutedEventArgs e)
		{
			_menuViewModel.Initialize();
		}

		private void OnControlUnloaded(object sender, RoutedEventArgs e)
		{
			_menuViewModel.Uninitialize();
		}
	}
}