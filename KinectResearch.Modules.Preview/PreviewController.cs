using System;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Preview.Views;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Preview
{
	public class PreviewController : IPreviewController
	{
		private readonly IUnityContainer _unityContainer;

		public PreviewController(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IPreviewController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IColorPreviewView, ColorPreviewView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<ISkeletonPreviewView, SkeletonPreviewView>(new ContainerControlledLifetimeManager());
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}