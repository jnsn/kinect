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

		public static string Print(this Vector3 vector)
		{
			return string.Format("X: {0}; Y: {1}; Z: {2}", vector.X, vector.Y, vector.Z);
		}
	}
}