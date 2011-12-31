using System;
using System.Collections.Generic;
using KinectResearch.Infrastructure;
using KinectResearch.Modules.Core.Utils;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Core.Gestures
{
	public abstract class AbstractGestureDetector
	{
		private DateTime _lastGestureTime = DateTime.Now;

		protected AbstractGestureDetector(int gestureCount = 20)
		{
			GestureCount = gestureCount;

			Entries = new List<Entry>();
			MinimalPeriodBetweenGestures = 0;
		}

		protected List<Entry> Entries { get; private set; }

		public int GestureCount { get; private set; }

		public int MinimalPeriodBetweenGestures { get; set; }

		public event Action<Gesture> GestureDetected;

		public void RaiseGestureDetected(Gesture gesture)
		{
			if (DateTime.Now.Subtract(_lastGestureTime).TotalMilliseconds > MinimalPeriodBetweenGestures)
			{
				var handler = GestureDetected;
				if (handler != null)
				{
					handler(gesture);
				}

				_lastGestureTime = DateTime.Now;
			}

			Entries.Clear();
		}

		public virtual void Add(Vector position, SkeletonEngine engine)
		{
			var entry = new Entry {Position = position.ToVector3(), Time = DateTime.Now};
			Entries.Add(entry);

			if (Entries.Count > GestureCount)
			{
				Entries.RemoveAt(0);
			}

			LookForGesture();
		}

		protected abstract void LookForGesture();
	}
}