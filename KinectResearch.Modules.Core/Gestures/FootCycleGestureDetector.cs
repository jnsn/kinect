using System.Linq;
using KinectResearch.Infrastructure;

namespace KinectResearch.Modules.Core.Gestures
{
	public class FootCycleGestureDetector : AbstractGestureDetector
	{
		public FootCycleGestureDetector(int gestureCount = 4)
			: base(gestureCount)
		{
		}

		protected override void LookForGesture()
		{
			if (Entries.Count > 1)
			{
				int cy = (int) (Entries.Last().Position.Y * 100.0f);
				int py = (int) (Entries[Entries.Count - 2].Position.Y * 100.0f);

				int difference = cy - py;

				if (difference > 1)
				{
					RaiseGestureDetected(Gesture.FootUp);
				}
				else if (difference < -1)
				{
					RaiseGestureDetected(Gesture.FootDown);
				}
			}
		}
	}
}