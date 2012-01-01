using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Details
{
	public class DetailsModule : IModule
	{
		private readonly IUnityContainer _unityContainer;

		public DetailsModule(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IModule Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IDetailsController, DetailsController>(new ContainerControlledLifetimeManager());
		}

		#endregion
	}
}