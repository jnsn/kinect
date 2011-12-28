using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Infrastructure.Events
{
	public class SkeletonFrameUpdate : CompositePresentationEvent<SkeletonData>
	{
	}
}