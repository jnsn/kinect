using System;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Details.Services;
using KinectResearch.Modules.Details.Views;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Details
{
	public class DetailsController : IDetailsController
	{
		private readonly IUnityContainer _unityContainer;

		public DetailsController(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		#region IDetailsController Members

		public void Initialize()
		{
			_unityContainer.RegisterType<IFootDetailsService, FootDetailsService>(new ContainerControlledLifetimeManager());

			_unityContainer.RegisterType<IDetailsView, DetailsView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IDetailsViewModel, DetailsViewModel>(new ContainerControlledLifetimeManager());

			// Initialize foot details service.
			_unityContainer.Resolve<IFootDetailsService>();
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}