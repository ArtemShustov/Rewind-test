using Game.Rewind;
using UnityEngine;

namespace Game {
	public class TestRewindableObject: MonoBehaviour, IRewandableObject {
		private Vector2Rewindable _postions = new Vector2Rewindable();
		private Vector3Rewindable _rotations = new Vector3Rewindable();

		public void Save(float time) {
			_postions.Add(time, transform.position);
			_rotations.Add(time, transform.eulerAngles);
		}
		public void Load(float time) { 
			transform.position = _postions.GetInterpolated(time);
			transform.eulerAngles = _rotations.GetInterpolated(time);
		}

		[SerializeField] private float _time;
		[SerializeField] private int _fontSize = 48;
		private void Update() {
			_time = Mathf.Clamp(_time + Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 60);
			if (Input.GetKeyDown(KeyCode.S)) {
				Save(_time);
			}
			if (Input.GetKey(KeyCode.L)) {
				Load(_time);
			}
		}
		private void OnGUI() {
			var style = GUI.skin.label;
			style.fontSize = _fontSize;
			GUILayout.TextField($"Time: {_time}", style);
			foreach (var snap in _postions.List) {
				GUILayout.TextField($" * {snap.Value} at {snap.Time}", style);
			}
		}
	}
}