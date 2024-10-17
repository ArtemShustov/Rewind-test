using System.Collections.Generic;
using System.Linq;

namespace Game.Rewind {
	public class Rewindable<T> {
		private List<Snap<T>> _snaps = new List<Snap<T>>();

		public IReadOnlyList<Snap<T>> List => _snaps.AsReadOnly();

		public void Add(float time, T value) {
			_snaps.Add(new Snap<T>(time, value));
			if (_snaps.Count >= 2 && _snaps[_snaps.Count - 2].Time > time) {
				_snaps.Sort((x, y) => x.Time.CompareTo(y.Time));
			}
		}
		public T Get(float time) {
			return _snaps.First((i) => i.Time == time).Value;
		}

		public struct Snap<T> {
			public float Time;
			public T Value;

			public Snap(float time, T value) {
				Time = time;
				Value = value;
			}
		}
	}
}