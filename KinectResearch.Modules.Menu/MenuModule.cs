using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Menu
{
	public class MenuModule : IModule
	{
		private readonly IUnityContainer _unityContainer;

		public MenuModule(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IModule Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IMenuController, MenuController>(new ContainerControlledLifetimeManager());
		}

		#endregion
	}
}