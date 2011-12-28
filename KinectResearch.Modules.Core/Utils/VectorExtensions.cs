using KinectResearch.Modules.Core.Gestures;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Core.Utils
{
	public static class VectorExtensions
	{
		 public static Vector3 ToVector3(this Vector vector)
		 {
		 	return new Vector3(vector.X, vector.Y, vector.Z);
		 }
	}
}