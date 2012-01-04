using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Core.Services;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Core
{
	public class CoreController : ICoreController
	{
		private readonly IUnityContainer _unityContainer;

		public CoreController(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region ICoreController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IKinectService, ConcreteKinectService>(new ContainerControlledLifetimeManager());

			_unityContainer.Resolve<IKinectService>().Initialize();
		}

		public void Uninitialize()
		{
			_unityContainer.Resolve<IKinectService>().Uninitialize();
		}

		#endregion
	}
}