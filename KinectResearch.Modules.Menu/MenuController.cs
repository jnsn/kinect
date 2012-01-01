using System;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Menu.Services;
using KinectResearch.Modules.Menu.Views;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Menu
{
	public class MenuController : IMenuController
	{
		private readonly IUnityContainer _unityContainer;

		public MenuController(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IMenuController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IMenuService, MenuService>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IMenuView, MenuView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IMenuViewModel, MenuViewModel>(new ContainerControlledLifetimeManager());

			// Initialize service.
			_unityContainer.Resolve<IMenuService>();
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}