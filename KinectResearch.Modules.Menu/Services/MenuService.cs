using System.Collections.Generic;
using System.Linq;
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
		private readonly IEventAggregator _eventAggregator;
		private readonly IKinectService _kinectService;

		private readonly AbstractGestureDetector _swipeGestureDetector = new SwipeGestureDetector();
		private readonly BarycenterHelper _barycenterHelper = new BarycenterHelper();

		public MenuService(IEventAggregator eventAggregator, IKinectService kinectService)
		{
			_eventAggregator = eventAggregator;
			_kinectService = kinectService;

			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Subscribe(OnSkeletonFrameUpdate);
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
					_swipeGestureDetector.Add(joint.Position, _kinectService.Kinect.SkeletonEngine);
				}
			}
		}
	}
}