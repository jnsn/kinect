﻿using Microsoft.Practices.Prism.Modularity;
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
			_unityContainer.RegisterType<IPreviewController, PreviewController>(new ContainerControlledLifetimeManager());
		}

		#endregion
	}
}