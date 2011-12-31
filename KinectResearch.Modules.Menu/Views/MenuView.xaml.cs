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
			InitializeComponent();
		}

		public MenuView(IMenuViewModel menuViewModel)
			: this()
		{
			_menuViewModel = menuViewModel;

			DataContext = _menuViewModel;
		}
	}
}