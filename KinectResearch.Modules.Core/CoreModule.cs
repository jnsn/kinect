using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Core.Services;
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

			_unityContainer.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IKinectService, ConcreteKinectService>(new ContainerControlledLifetimeManager());
		}

		#region IModule Members

		public void Initialize()
		{
			_unityContainer.Resolve<IKinectService>().Initialize();
		}

		#endregion

		public void Uninitialize()
		{
			_unityContainer.Resolve<IKinectService>().Uninitialize();
		}
	}
}