using System.Linq;

namespace Game.Rewind {
	public class FloatRewindable: Rewindable<float> {
		public float GetInterpolated(float time) {
			var snaps = List;

			if (snaps.Count == 0) throw new System.InvalidOperationException();
			if (time <= snaps.First().Time) return snaps.First().Value;
			if (time >= snaps.Last().Time) return snaps.Last().Value;

			Snap<float> before = snaps.Last(snap => snap.Time <= time);
			Snap<float> after = snaps.First(snap => snap.Time >= time);

			if (before.Time == time) return before.Value;
			if (after.Time == time) return after.Value;

			float t = (time - before.Time) / (after.Time - before.Time);
			return before.Value + t * (after.Value - before.Value);
		}
	}
}