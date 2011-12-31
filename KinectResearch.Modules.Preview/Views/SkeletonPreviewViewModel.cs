using Coding4Fun.Kinect.Wpf;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Preview.Views
{
	public class SkeletonPreviewViewModel : AbstractViewModel
	{
		private readonly IEventAggregator _eventAggregator;
		private double _actualControlHeight;
		private double _actualControlWidth;

		private double _headLeft;
		private double _headTop;
		private double _leftHandLeft;
		private double _leftHandTop;
		private double _rightHandLeft;
		private double _rightHandTop;

		public SkeletonPreviewViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;

			HeadLeft = 0.0;
			HeadTop = 0.0;

			LeftHandLeft = 50.0;
			LeftHandTop = 0.0;

			RightHandLeft = 100.0;
			RightHandTop = 0.0;
		}

		public double ActualControlWidth
		{
			get { return _actualControlWidth; }
			set
			{
				if (_actualControlWidth != value)
				{
					_actualControlWidth = value;
					NotifyPropertyChanged("ActualControlWidth");
				}
			}
		}

		public double ActualControlHeight
		{
			get { return _actualControlHeight; }
			set
			{
				if (_actualControlHeight != value)
				{
					_actualControlHeight = value;
					NotifyPropertyChanged("ActualControlHeight");
				}
			}
		}

		public double HeadLeft
		{
			get { return _headLeft; }
			set
			{
				if (_headLeft != value)
				{
					_headLeft = value;
					NotifyPropertyChanged("HeadLeft");
				}
			}
		}

		public double HeadTop
		{
			get { return _headTop; }
			set
			{
				if (_headTop != value)
				{
					_headTop = value;
					NotifyPropertyChanged("HeadTop");
				}
			}
		}

		public double LeftHandLeft
		{
			get { return _leftHandLeft; }
			set
			{
				if (_leftHandLeft != value)
				{
					_leftHandLeft = value;
					NotifyPropertyChanged("LeftHandLeft");
				}
			}
		}

		public double LeftHandTop
		{
			get { return _leftHandTop; }
			set
			{
				if (_leftHandTop != value)
				{
					_leftHandTop = value;
					NotifyPropertyChanged("LeftHandTop");
				}
			}
		}

		public double RightHandLeft
		{
			get { return _rightHandLeft; }
			set
			{
				if (_rightHandLeft != value)
				{
					_rightHandLeft = value;
					NotifyPropertyChanged("RightHandLeft");
				}
			}
		}

		public double RightHandTop
		{
			get { return _rightHandTop; }
			set
			{
				if (_rightHandTop != value)
				{
					_rightHandTop = value;
					NotifyPropertyChanged("RightHandTop");
				}
			}
		}

		public void Initialize()
		{
			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Subscribe(OnSkeletonFrameUpdate);
		}

		public void Uninitialize()
		{
			_eventAggregator.GetEvent<SkeletonFrameUpdate>().Unsubscribe(OnSkeletonFrameUpdate);
		}

		private void OnSkeletonFrameUpdate(SkeletonData data)
		{
			//int width = (int) ActualControlWidth;
			//int height = (int) ActualControlHeight;
			const int WIDTH = 480;
			const int HEIGHT = 320;

			var scaledHead = data.Joints[JointID.Head].ScaleTo(WIDTH, HEIGHT, .7f, .7f);
			var scaledLeftHand = data.Joints[JointID.HandLeft].ScaleTo(WIDTH, HEIGHT, .7f, .7f);
			var scaledRightHand = data.Joints[JointID.HandRight].ScaleTo(WIDTH, HEIGHT, .7f, .7f);

			HeadLeft = scaledHead.Position.X;
			HeadTop = scaledHead.Position.Y;

			LeftHandLeft = scaledLeftHand.Position.X;
			LeftHandTop = scaledLeftHand.Position.Y;

			RightHandLeft = scaledRightHand.Position.X;
			RightHandTop = scaledRightHand.Position.Y;
		}
	}
}