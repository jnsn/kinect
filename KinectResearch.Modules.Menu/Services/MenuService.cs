using System.Linq;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Core.Gestures;
using KinectResearch.Modules.Core.Utils;
using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Menu.Services
{
	public class MenuService : IMenuService
	{
		private readonly BarycenterHelper _barycenterHelper = new BarycenterHelper();
		private readonly IEventAggregator _eventAggregator;
		private readonly AbstractGestureDetector _gestureDetector = new SwipeGestureDetector();
		private readonly IKinectService _kinectService;

		private MenuStatus _menuStatus = MenuStatus.Close;

		public MenuService(IEventAggregator eventAggregator, IKinectService kinectService)
		{
			_eventAggregator = eventAggregator;
			_kinectService = kinectService;

			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Subscribe(OnSkeletonFrameUpdate);

			_gestureDetector.MinimalPeriodBetweenGestures = 1000;
			_gestureDetector.GestureDetected += OnGestureDetected;
		}

		private void OnGestureDetected(Gesture gesture)
		{
			if (gesture == Gesture.Right)
			{
				_menuStatus = _menuStatus == MenuStatus.Close ? MenuStatus.Open : MenuStatus.Close;
				_eventAggregator.GetEvent<SwitchMenu>().Publish(_menuStatus);
			}
		}

		private void OnSkeletonFrameUpdate(SkeletonData data)
		{
			_barycenterHelper.Add(data.Position.ToVector3(), data.TrackingID);

			if (_barycenterHelper.IsStable(data.TrackingID))
			{
				var joints = data.Joints
					.Cast<Joint>()
					.Where(joint => (joint.Position.W >= .8f) && (joint.TrackingState == JointTrackingState.Tracked))
					.Where(joint => joint.ID == JointID.HandRight);

				foreach (var joint in joints)
				{
					_gestureDetector.Add(joint.Position, _kinectService.Kinect.SkeletonEngine);
				}
			}
		}
	}
}