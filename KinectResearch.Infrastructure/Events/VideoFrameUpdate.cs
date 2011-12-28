using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Events;

namespace KinectResearch.Infrastructure.Events
{
	public class VideoFrameUpdate : CompositePresentationEvent<BitmapSource>
	{
	}
}