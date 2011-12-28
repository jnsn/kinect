using KinectResearch.Modules.Preview.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace KinectResearch.Modules.Preview
{
	public class PreviewModule : IModule
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _unityContainer;

		public PreviewModule(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		#region IModule Members

		public void Initialize()
		{
			var view = _unityContainer.Resolve<ColorPreviewView>();
			_regionManager.Regions["RightRegion"].Add(view);
		}

		#endregion
	}
}