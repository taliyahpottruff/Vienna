using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManager : MonoBehaviour {
	public Dictionary<string, AudioClip> sounds;

	private AudioSource source;

	private void Awake() {
		source = GetComponent<AudioSource>();

		sounds = new Dictionary<string, AudioClip>() {
			{"menuHover", Resources.Load<AudioClip>("Audio/Effects/Pop")}
		};
	}

	public void PlaySoundEffect(string name) {
		source.PlayOneShot(sounds[name]);
	}
}