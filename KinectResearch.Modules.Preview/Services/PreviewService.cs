using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Preview.Services
{
	public class PreviewService : IPreviewService
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		private IColorPreviewView _colorPreviewView;
		private ISkeletonPreviewView _skeletonPreviewView;

		public PreviewService(IUnityContainer unityContainer, IEventAggregator eventAggregator, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_eventAggregator = eventAggregator;
			_regionManager = regionManager;
		}

		#region IPreviewService Members

		public void Initialize()
		{
			_skeletonPreviewView = _unityContainer.Resolve<ISkeletonPreviewView>();
			_colorPreviewView = _unityContainer.Resolve<IColorPreviewView>();

			_regionManager.Regions["RightRegion"].Add(_skeletonPreviewView);
			_regionManager.Regions["RightRegion"].Add(_colorPreviewView);

			OnSwitchPreviewView(PreviewView.Color);

			_eventAggregator.GetEvent<SwitchPreviewView>().Subscribe(OnSwitchPreviewView);
		}

		public void Uninitialize()
		{
			_eventAggregator.GetEvent<SwitchPreviewView>().Unsubscribe(OnSwitchPreviewView);
		}

		#endregion

		private void OnSwitchPreviewView(PreviewView view)
		{
			switch (view)
			{
				case PreviewView.Skeleton:
					_regionManager.Regions["RightRegion"].Deactivate(_colorPreviewView);
					_regionManager.Regions["RightRegion"].Activate(_skeletonPreviewView);
					break;
				case PreviewView.Color:
					_regionManager.Regions["RightRegion"].Deactivate(_skeletonPreviewView);
					_regionManager.Regions["RightRegion"].Activate(_colorPreviewView);
					break;
			}
		}
	}
}