using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Infrastructure.Interfaces
{
	public interface IKinectService
	{
		Runtime Kinect { get; }

		void Initialize();
		void Uninitialize();
	}
}