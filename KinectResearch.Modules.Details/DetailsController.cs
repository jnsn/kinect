using System;
using KinectResearch.Infrastructure.Interfaces;
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
			_unityContainer.RegisterType<IDetailsView, DetailsView>(new ContainerControlledLifetimeManager());
			_unityContainer.RegisterType<IDetailsViewModel, DetailsViewModel>(new ContainerControlledLifetimeManager());
		}

		public void Uninitialize()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}