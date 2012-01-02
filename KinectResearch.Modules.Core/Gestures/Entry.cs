using System;
using KinectResearch.Modules.Core.Utils;

namespace KinectResearch.Modules.Core.Gestures
{
	public class Entry
	{
		public DateTime Time { get; set; }

		public Vector3 Position { get; set; }

		public override string ToString()
		{
			return string.Format("{0}; Time: {1}", Position.Print(), Time);
		}
	}
}