using System;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Details.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Details
{
	public class DetailsController : IDetailsController
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public DetailsController(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		#region IDetailsController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IDetailsView, DetailsView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IDetailsViewModel, DetailsViewModel>(new ContainerControlledLifetimeManager());

			var view = _unityContainer.Resolve<IDetailsView>();
			_regionManager.Regions["LeftRegion"].Add(view);
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}