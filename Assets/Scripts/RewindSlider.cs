using UnityEngine;
using UnityEngine.UI;

namespace Game {
	public class RewindSlider: MonoBehaviour {
		[SerializeField] private Slider _slider;
		[SerializeField] private LevelRewind _rewind;

		private void OnValueChanged(float value) {
			_rewind.LoadNormalized(value);
		}

		private void OnEnable() {
			_slider.onValueChanged.AddListener(OnValueChanged);
		}
		private void OnDisable() {
			_slider.onValueChanged.RemoveListener(OnValueChanged);
		}
	}
}