using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour {
	public VolumeType type;

	private Slider slider;

	private void Awake() {
		slider = GetComponent<Slider>();
	}

	private void OnEnable() {
		switch (type) {
			case VolumeType.Master:
				slider.value = GameManager.singleton.MasterVolume;
				break;
			case VolumeType.Music:
				slider.value = GameManager.singleton.GetMusicVolumeRaw();
				break;
			case VolumeType.Sound:
				slider.value = GameManager.singleton.GetSoundVolumeRaw();
				break;
		}
	}

	public void OnChanged(float value) {
		switch (type) {
			case VolumeType.Master:
				GameManager.singleton.MasterVolume = value;
				break;
			case VolumeType.Music:
				GameManager.singleton.MusicVolume = value;
				break;
			case VolumeType.Sound:
				GameManager.singleton.SoundVolume = value;
				break;
		}
	}
}

public enum VolumeType {
	Master, Sound, Music
}