using System.Linq;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Modules.Core.Gestures;
using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Details.Services
{
	public class FootDetailsService : IFootDetailsService
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IKinectService _kinectService;
		private readonly AbstractGestureDetector _leftFootGestureDetector = new FootCycleGestureDetector();
		private readonly AbstractGestureDetector _rightFootGestureDetector = new FootCycleGestureDetector();

		public FootDetailsService(IEventAggregator eventAggregator, IKinectService kinectService)
		{
			_eventAggregator = eventAggregator;
			_kinectService = kinectService;

			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Subscribe(OnSkeletonFrameUpdate);

			_rightFootGestureDetector.MinimalPeriodBetweenGestures = 0;
			_rightFootGestureDetector.GestureDetected += OnRightFootGestureDetected;
			_leftFootGestureDetector.MinimalPeriodBetweenGestures = 0;
			_leftFootGestureDetector.GestureDetected += OnLeftFootGestureDetected;
		}

		private void OnRightFootGestureDetected(Gesture gesture)
		{
			switch (gesture)
			{
				case Gesture.FootUp:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.RightUp);
					break;
				case Gesture.FootDown:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.RightDown);
					break;
				case Gesture.None:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.RightNone);
					break;
			}
		}

		private void OnLeftFootGestureDetected(Gesture gesture)
		{
			switch (gesture)
			{
				case Gesture.FootUp:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.LeftUp);
					break;
				case Gesture.FootDown:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.LeftDown);
					break;
				case Gesture.None:
					_eventAggregator.GetEvent<FootPositionChanged>().Publish(FootMovement.LeftNone);
					break;
			}
		}

		private void OnSkeletonFrameUpdate(SkeletonData data)
		{
			var joints = data.Joints
				.Cast<Joint>()
				.Where(joint => (joint.Position.W >= .8f) && (joint.TrackingState == JointTrackingState.Tracked))
				.ToList();

			if (joints.Count > 0)
			{
				_rightFootGestureDetector.Add(joints.FirstOrDefault(j => j.ID == JointID.KneeRight).Position, _kinectService.Kinect.SkeletonEngine);
				_leftFootGestureDetector.Add(joints.FirstOrDefault(j => j.ID == JointID.KneeLeft).Position, _kinectService.Kinect.SkeletonEngine);
			}
		}
	}
}