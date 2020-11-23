using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vienna {
	[RequireComponent(typeof(AudioSource))]
	public class SoundEffectsManager : MonoBehaviour {
		public static SoundEffectsManager singleton;

		public Dictionary<string, AudioClip> sounds;

		private AudioSource source;

		private void Awake() {
			singleton = this;

			source = GetComponent<AudioSource>();

			sounds = new Dictionary<string, AudioClip>() {
			{"menuHover", Resources.Load<AudioClip>("Audio/Effects/Pop")}
		};
		}

		public void PlaySoundEffect(string name) {
			source.volume = GameManager.singleton.SoundVolume;
			source.PlayOneShot(sounds[name]);
		}
	}
}