using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	[SerializeField]
	private GameObject loadingScreen;
	[SerializeField]
	private Slider progressBar;

	private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
	private float totalSceneProgress = 0f, masterVolume = 1f, soundVolume = 1f, musicVolume = 1f;

	public float SoundVolume {
		get {
			return masterVolume * soundVolume;
		}
		set {
			soundVolume = Mathf.Clamp01(value);
		}
	}

	public float MusicVolume {
		get {
			return masterVolume * musicVolume;
		}
		set {
			musicVolume = Mathf.Clamp01(value);
		}
	}

	public float MasterVolume {
		get {
			return masterVolume;
		}
		set {
			masterVolume = Mathf.Clamp01(value);
		}
	}

	private void Awake() {
		singleton = this;

		if (SceneManager.sceneCount < 2) {
			SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
		}
	}

	public void LoadGame() {
		loadingScreen.SetActive(true);
		scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
		scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));

		//Start checking the loading progress
		StartCoroutine(GetSceneLoadProgress());
	}

	public float GetSoundVolumeRaw() {
		return soundVolume;
	}

	public float GetMusicVolumeRaw() {
		return musicVolume;
	}

	private IEnumerator GetSceneLoadProgress() {
		foreach (AsyncOperation op in scenesLoading) {
			while (!op.isDone) {
				totalSceneProgress = 0f;

				foreach (AsyncOperation op1 in scenesLoading) {
					totalSceneProgress += op1.progress;
				}

				totalSceneProgress = totalSceneProgress / scenesLoading.Count;
				progressBar.value = totalSceneProgress;

				yield return null;
			}
		}

		loadingScreen.gameObject.SetActive(false);
	}
}

public enum SceneIndexes {
	MANAGER = 0,
	MAIN_MENU = 1,
	GAME = 2
}