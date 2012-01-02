using System;
using System.Windows.Media.Imaging;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;

namespace KinectResearch.Modules.Details.Views
{
	public class DetailsViewModel : AbstractViewModel, IDetailsViewModel
	{
		private readonly IEventAggregator _eventAggregator;

		private FootMovement _footLeftMovement;
		private FootMovement _footRightMovement;
		private int _frameRate = -1;
		private DateTime _lastFrameDate = DateTime.MaxValue;
		private int _lastFrames;
		private int _totalFrames;

		public DetailsViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public FootMovement FootLeftMovement
		{
			get { return _footLeftMovement; }
			set
			{
				if (_footLeftMovement != value)
				{
					_footLeftMovement = value;
					NotifyPropertyChanged("FootLeftMovement");
				}
			}
		}

		public FootMovement FootRightMovement
		{
			get { return _footRightMovement; }
			set
			{
				if (_footRightMovement != value)
				{
					_footRightMovement = value;
					NotifyPropertyChanged("FootRightMovement");
				}
			}
		}

		public int FrameRate
		{
			get { return _frameRate; }
			set
			{
				if (_frameRate != value)
				{
					_frameRate = value;
					NotifyPropertyChanged("FrameRate");
				}
			}
		}

		#region IDetailsViewModel Members

		public void Initialize()
		{
			_eventAggregator.GetEvent<VideoFrameUpdate>().Subscribe(OnVideoFrameUpdate);
			_eventAggregator.GetEvent<FootPositionChanged>().Subscribe(OnFootMovement);
		}

		public void Uninitialize()
		{
			_eventAggregator.GetEvent<VideoFrameUpdate>().Unsubscribe(OnVideoFrameUpdate);
			_eventAggregator.GetEvent<FootPositionChanged>().Unsubscribe(OnFootMovement);
		}

		#endregion

		private void OnFootMovement(FootMovement movement)
		{
			switch (movement)
			{
				case FootMovement.LeftUp:
				case FootMovement.LeftDown:
				case FootMovement.LeftNone:
					FootLeftMovement = movement;
					break;
				case FootMovement.RightDown:
				case FootMovement.RightUp:
				case FootMovement.RightNone:
					FootRightMovement = movement;
					break;
			}
		}

		private void OnVideoFrameUpdate(BitmapSource source)
		{
			CalculateFrameRate();
		}

		private void CalculateFrameRate()
		{
			++_totalFrames;

			var now = DateTime.Now;
			if ((_lastFrameDate == DateTime.MaxValue) || (now.Subtract(_lastFrameDate) > TimeSpan.FromSeconds(1)))
			{
				FrameRate = _totalFrames - _lastFrames;

				_lastFrames = _totalFrames;
				_lastFrameDate = now;
			}
		}
	}
}