using System;

namespace KinectResearch.Infrastructure
{
	public class KinectException : Exception
	{
		public KinectException(string message) : base(message)
		{
		}
	}
}