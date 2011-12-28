using System;
using System.Collections.Generic;
using KinectResearch.Modules.Core.Utils;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Core.Gestures
{
	public abstract class AbstractGestureDetector
	{
		//private DateTime _lastGestureTime = DateTime.Now;

		protected AbstractGestureDetector(int gestureCount = 20)
		{
			Entries = new List<Entry>();
			MinimalPeriodBetweenGestures = 0;
			GestureCount = gestureCount;
		}

		protected List<Entry> Entries { get; private set; }

		public int GestureCount { get; private set; }

		public int MinimalPeriodBetweenGestures { get; set; }

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