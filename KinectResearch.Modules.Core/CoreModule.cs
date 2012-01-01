using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Core
{
	public class CoreModule : IModule
	{
		private readonly IUnityContainer _unityContainer;

		public CoreModule(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IModule Members

		public void Initialize()
		{
			_unityContainer.RegisterType<ICoreController, CoreController>(new ContainerControlledLifetimeManager());
		}

		#endregion
	}
}