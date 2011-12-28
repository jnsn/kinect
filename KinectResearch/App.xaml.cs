using System.Windows;

namespace KinectResearch
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private Bootstrapper _bootstrapper;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_bootstrapper = new Bootstrapper();
			_bootstrapper.Run();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_bootstrapper.UninitializeModules();

			base.OnExit(e);
		}
	}
}