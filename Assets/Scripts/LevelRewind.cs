using Game.Rewind;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game {
	public class LevelRewind: MonoBehaviour {
		[SerializeField] private float _minTime = 0;
		[SerializeField] private float _maxTime = 1;
		[SerializeField] private Transform _root;
		private List<IRewandableObject> _rewandables;

		private void Awake() {
			_rewandables = _root.GetComponentsInChildren<IRewandableObject>().ToList();
		}

		public void Load(float time) {
			_rewandables.ForEach(x => x.Load(time));
		}
		public void LoadNormalized(float normalized) {
			normalized = Mathf.Clamp01(normalized);
			var time = Mathf.Lerp(_minTime, _maxTime, normalized);
			Load(time);
		}

		private void OnValidate() {
			_maxTime = Mathf.Max(_maxTime, _minTime);
		}
	}
}