using System.Collections.Generic;
using KinectResearch.Modules.Core.Gestures;

namespace KinectResearch.Modules.Core.Utils
{
	public class BarycenterHelper
	{
		private readonly Dictionary<int, List<Vector3>> _positions = new Dictionary<int, List<Vector3>>();
		private readonly int _windowSize;

		public BarycenterHelper(int windowSize = 20, float treshHold = .05f)
		{
			_windowSize = windowSize;
			TreshHold = treshHold;
		}

		public float TreshHold { get; set; }

		public void Add(Vector3 position, int trackingID)
		{
			if (!_positions.ContainsKey(trackingID))
			{
				_positions.Add(trackingID, new List<Vector3>());
			}

			_positions[trackingID].Add(position);

			if (_positions[trackingID].Count > _windowSize)
			{
				_positions[trackingID].RemoveAt(0);
			}
		}

		public bool IsStable(int trackingID)
		{
			var current = _positions[trackingID];
			if (current.Count != _windowSize)
			{
				return false;
			}

			var position = current[current.Count - 1];

			for (int i = 0; i < current.Count - 2; i++)
			{
				if ((current[i] - position).Length > TreshHold)
				{
					return false;
				}
			}

			return true;
		}
	}
}