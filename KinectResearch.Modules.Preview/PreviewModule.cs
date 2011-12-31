using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Preview.Services;
using KinectResearch.Modules.Preview.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Preview
{
	public class PreviewModule : IModule
	{
		private readonly IUnityContainer _unityContainer;

		public PreviewModule(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IModule Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IColorPreviewView, ColorPreviewView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<ISkeletonPreviewView, SkeletonPreviewView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IPreviewService, PreviewService>(new ContainerControlledLifetimeManager());

			_unityContainer.Resolve<IPreviewService>().Initialize();
		}

		#endregion

		public void Uninitialize()
		{
			_unityContainer.Resolve<IPreviewService>().Uninitialize();
		}
	}
}