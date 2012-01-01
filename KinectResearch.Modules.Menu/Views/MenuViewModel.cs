using System.Linq;
using Coding4Fun.Kinect.Wpf;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Menu.Views
{
	public class MenuViewModel : AbstractViewModel, IMenuViewModel
	{
		private readonly IEventAggregator _eventAggregator;

		private double _jointX;
		private double _jointY;

		public MenuViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;

			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Subscribe(OnSkeletonFrameUpdate);

			SwitchToSkeletonPreviewCommand = new DelegateCommand(OnSwitchToSkeletonPreviewCommand);
			SwitchToColorPreviewCommand = new DelegateCommand(OnSwitchToColorPreviewCommand);
		}

		public DelegateCommand SwitchToSkeletonPreviewCommand { get; private set; }

		public DelegateCommand SwitchToColorPreviewCommand { get; private set; }

		public double JointX
		{
			get { return _jointX; }
			set
			{
				if (_jointX != value)
				{
					_jointX = value;
					NotifyPropertyChanged("JointX");
				}
			}
		}

		public double JointY
		{
			get { return _jointY; }
			set
			{
				if (_jointY != value)
				{
					_jointY = value;
					NotifyPropertyChanged("JointY");
				}
			}
		}

		private void OnSkeletonFrameUpdate(SkeletonData data)
		{
			var joints = data.Joints
				.Cast<Joint>()
				.Where(joint => (joint.Position.W >= .8f) && (joint.TrackingState == JointTrackingState.Tracked))
				.Where(joint => joint.ID == JointID.HandRight);

			foreach (var scaled in joints.Select(joint => joint.ScaleTo(640, 480, 0.5f, 0.5f)))
			{
				JointX = scaled.Position.X;
				JointY = scaled.Position.Y;
			}
		}

		private void OnSwitchToSkeletonPreviewCommand()
		{
			var view = _eventAggregator.GetEvent<SwitchPreviewView>();
			view.Publish(PreviewView.Skeleton);
		}

		private void OnSwitchToColorPreviewCommand()
		{
			_eventAggregator.GetEvent<SwitchPreviewView>().Publish(PreviewView.Color);
		}
	}
}