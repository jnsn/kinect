using System.Windows;
using KinectResearch.Modules.Core;
using KinectResearch.Modules.Details;
using KinectResearch.Modules.Menu;
using KinectResearch.Modules.Preview;
using KinectResearch.Services;
using Microsoft.Practices.Prism.Modularity;
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

		protected override IModuleCatalog CreateModuleCatalog()
		{
			return new ModuleCatalog()
				.AddModule(typeof (CoreModule))
				.AddModule(typeof (MenuModule), "CoreModule")
				.AddModule(typeof (PreviewModule), "CoreModule")
				.AddModule(typeof (DetailsModule), "CoreModule");
		}

		protected override void InitializeModules()
		{
			base.InitializeModules();

			Container.RegisterType<IViewService, ViewService>(new ContainerControlledLifetimeManager());

			Container.Resolve<ICoreController>().Initialize();
			Container.Resolve<IPreviewController>().Initialize();
			Container.Resolve<IMenuController>().Initialize();
			Container.Resolve<IDetailsController>().Initialize();

			// Initialize view service.
			Container.Resolve<IViewService>().Initialize();
		}

		public void UninitializeModules()
		{
			// Uninitialize view service.
			Container.Resolve<IViewService>().Uninitialize();

			Container.Resolve<ICoreController>().Uninitialize();
		}
	}
}