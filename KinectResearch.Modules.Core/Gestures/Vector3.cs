using System;

namespace KinectResearch.Modules.Core.Gestures
{
	[Serializable]
	public struct Vector3
	{
		public static Vector3 Zero = new Vector3(0.0f, 0.0f, 0.0f);

		public float X;
		public float Y;
		public float Z;

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public float Length
		{
			get { return (float) Math.Sqrt(X*X + Y*Y + Z*Z); }
		}

		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Vector3 operator *(Vector3 left, float value)
		{
			return new Vector3(left.X*value, left.Y*value, left.Z*value);
		}

		public static Vector3 operator *(float value, Vector3 left)
		{
			return left*value;
		}

		public static Vector3 operator /(Vector3 left, float value)
		{
			return new Vector3(left.X/value, left.Y/value, left.Z/value);
		}
	}
}