using System;

namespace KinectResearch.Modules.Core.Gestures
{
	public class SwipeGestureDetector : AbstractGestureDetector
	{
		private const float SWIPE_MINIMAL_LENGTH = 0.4f;
		private const float SWIPE_MAXIMAL_HEIGHT = 0.2f;
		private const int SWIPE_MININAL_DURATION = 250;
		private const int SWIPE_MAXIMAL_DURATION = 1500;

		public SwipeGestureDetector(int gestureCount = 20)
			: base(gestureCount)
		{
		}

		protected override void LookForGesture()
		{
			Func<Vector3, Vector3, bool> heightFunction = (a, b) => Math.Abs(b.Y - a.Y) < SWIPE_MAXIMAL_HEIGHT;
			Func<Vector3, Vector3, bool> directionFunction = (a, b) => b.X - a.X > -.01f;
			Func<Vector3, Vector3, bool> lengthFunction = (a, b) => Math.Abs(b.X - a.X) > SWIPE_MINIMAL_LENGTH;

			if (ScanPosition(heightFunction, directionFunction, lengthFunction))
			{
				Console.WriteLine("{0} - SWIPE RIGHT", DateTime.Now);
			}
		}

		private bool ScanPosition(
			Func<Vector3, Vector3, bool> heightFunction,
			Func<Vector3, Vector3, bool> directionFunction,
			Func<Vector3, Vector3, bool> lengthFunction,
			int minimumTime = SWIPE_MININAL_DURATION,
			int maximumTime = SWIPE_MAXIMAL_DURATION)
		{
			int start = 0;
			for (int i = 1; i < Entries.Count - 1; i++)
			{
				if (!heightFunction(Entries[0].Position, Entries[i].Position) || !directionFunction(Entries[i].Position, Entries[i + 1].Position))
				{
					start = i;
				}

				if (lengthFunction(Entries[i].Position, Entries[start].Position))
				{
					double totalMilliseconds = (Entries[i].Time - Entries[start].Time).TotalMilliseconds;

					if ((totalMilliseconds >= minimumTime) && (totalMilliseconds <= maximumTime))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}