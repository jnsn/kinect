using System;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Menu.Services;
using KinectResearch.Modules.Menu.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Menu
{
	public class MenuController : IMenuController
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public MenuController(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		#region IMenuController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IMenuService, MenuService>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IMenuView, MenuView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IMenuViewModel, MenuViewModel>(new ContainerControlledLifetimeManager());

			// Initialize service.
			_unityContainer.Resolve<IMenuService>();

			var view = _unityContainer.Resolve<IMenuView>();
			_regionManager.Regions["LeftRegion"].Add(view);
			_regionManager.Regions["LeftRegion"].Deactivate(view);
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}