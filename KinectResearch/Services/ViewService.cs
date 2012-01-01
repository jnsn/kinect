using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace KinectResearch.Services
{
	public class ViewService : IViewService
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		private IColorPreviewView _colorPreviewView;
		private IDetailsView _detailsView;
		private IMenuView _menuView;
		private ISkeletonPreviewView _skeletonPreviewView;

		public ViewService(IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator eventAggregator)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
			_eventAggregator = eventAggregator;
		}

		#region IViewService Members

		public void Initialize()
		{
			_eventAggregator.GetEvent<SwitchPreviewView>().Subscribe(OnSwitchPreviewView);
			_eventAggregator.GetEvent<SwitchMenu>().Subscribe(OnSwitchMenu);

			LoadViews();
		}

		public void Uninitialize()
		{
			_eventAggregator.GetEvent<SwitchPreviewView>().Unsubscribe(OnSwitchPreviewView);
			_eventAggregator.GetEvent<SwitchMenu>().Unsubscribe(OnSwitchMenu);
		}

		#endregion

		private void LoadViews()
		{
			_skeletonPreviewView = _unityContainer.Resolve<ISkeletonPreviewView>();
			_colorPreviewView = _unityContainer.Resolve<IColorPreviewView>();
			_menuView = _unityContainer.Resolve<IMenuView>();
			_detailsView = _unityContainer.Resolve<IDetailsView>();

			_regionManager.Regions["LeftRegion"].Add(_detailsView);
			_regionManager.Regions["LeftRegion"].Add(_menuView);

			_regionManager.Regions["RightRegion"].Add(_skeletonPreviewView);
			_regionManager.Regions["RightRegion"].Add(_colorPreviewView);

			OnSwitchPreviewView(PreviewView.Color);
			OnSwitchMenu(MenuStatus.Close);
		}

		private void OnSwitchMenu(MenuStatus status)
		{
			switch (status)
			{
				case MenuStatus.Open:
					_regionManager.Regions["LeftRegion"].Deactivate(_detailsView);
					_regionManager.Regions["LeftRegion"].Activate(_menuView);
					break;
				case MenuStatus.Close:
					_regionManager.Regions["LeftRegion"].Deactivate(_menuView);
					_regionManager.Regions["LeftRegion"].Activate(_detailsView);
					break;
			}
		}

		private void OnSwitchPreviewView(PreviewView view)
		{
			switch (view)
			{
				case PreviewView.Color:
					_regionManager.Regions["RightRegion"].Deactivate(_skeletonPreviewView);
					_regionManager.Regions["RightRegion"].Activate(_colorPreviewView);
					break;
				case PreviewView.Skeleton:
					_regionManager.Regions["RightRegion"].Deactivate(_colorPreviewView);
					_regionManager.Regions["RightRegion"].Activate(_skeletonPreviewView);
					break;
			}
		}
	}
}