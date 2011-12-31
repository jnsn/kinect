using System.Windows;
using KinectResearch.Modules.Core;
using KinectResearch.Modules.Menu;
using KinectResearch.Modules.Preview;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace KinectResearch
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			var mainWindow = Container.Resolve<MainWindow>();
			mainWindow.Show();

			return mainWindow;
		}

		protected override void InitializeModules()
		{
			Container.Resolve<CoreModule>().Initialize();
			Container.Resolve<PreviewModule>().Initialize();
			Container.Resolve<MenuModule>().Initialize();
		}

		public void UninitializeModules()
		{
			Container.Resolve<CoreModule>().Uninitialize();
			Container.Resolve<PreviewModule>().Uninitialize();
		}
	}
}