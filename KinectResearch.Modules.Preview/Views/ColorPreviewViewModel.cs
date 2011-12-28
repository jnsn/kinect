using System.Windows.Media.Imaging;
using KinectResearch.Infrastructure;
using KinectResearch.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;

namespace KinectResearch.Modules.Preview.Views
{
	public class ColorPreviewViewModel : AbstractViewModel
	{
		private readonly IEventAggregator _eventAggregator;

		private BitmapSource _imageSource;
		private bool _inverted;

		public ColorPreviewViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public BitmapSource ImageSource
		{
			get { return _imageSource; }
			set
			{
				if (_imageSource != value)
				{
					_imageSource = value;
					NotifyPropertyChanged("ImageSource");
				}
			}
		}

		public bool Inverted
		{
			get { return _inverted; }
			set
			{
				if (_inverted != value)
				{
					_inverted = value;
					NotifyPropertyChanged("Inverted");
				}
			}
		}

		public void Initialize()
		{
			_eventAggregator.GetEvent<VideoFrameUpdate>().Subscribe(OnVideoFrameUpdate);
		}

		public void Uninitialize()
		{
			_eventAggregator.GetEvent<VideoFrameUpdate>().Unsubscribe(OnVideoFrameUpdate);
		}

		private void OnVideoFrameUpdate(BitmapSource image)
		{
			ImageSource = image;
		}
	}
}