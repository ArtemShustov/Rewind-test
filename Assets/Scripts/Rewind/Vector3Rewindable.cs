using System.Linq;
using UnityEngine;

namespace Game.Rewind {
	public class Vector3Rewindable: Rewindable<Vector3> {
		public Vector3 GetInterpolated(float time) {
			var snaps = List;

			if (snaps.Count == 0) throw new System.InvalidOperationException();
			if (time <= snaps.First().Time) return snaps.First().Value;
			if (time >= snaps.Last().Time) return snaps.Last().Value;

			Snap<Vector3> before = snaps.Last(snap => snap.Time <= time);
			Snap<Vector3> after = snaps.First(snap => snap.Time >= time);

			if (before.Time == time) return before.Value;
			if (after.Time == time) return after.Value;

			float t = (time - before.Time) / (after.Time - before.Time);
			return Vector3.Lerp(before.Value, after.Value, t);
		}
	}
}