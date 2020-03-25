using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	public bool paused;
	public new GameCamera camera;

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

	private void OnEnable() {
		//Try to find the player and set the camera target
		Player player = FindObjectOfType<Player>();
		if (player != null) camera.SetTarget(player.transform);
	}

	public void LoadGame() {
		UIManager uiManager = FindObjectOfType<UIManager>();
		if (uiManager != null) uiManager.SetPauseScreen(false);

		loadingScreen.SetActive(true);
		
		//Dynamically unload all scenes that are not the persistent scene
		for (int i = 0; i < SceneManager.sceneCount; i++) {
			if (SceneManager.GetSceneAt(i).buildIndex == (int)SceneIndexes.MANAGER) continue;

			scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneManager.GetSceneAt(i).buildIndex));
		}

		scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive));

		//Start checking the loading progress
		StartCoroutine(GetSceneLoadProgress());
	}

	public void QuitGame() {
		Debug.Log("Quitting game with Code 0");
		Application.Quit(0);
	}

	public void GameToMenu() {
		SceneManager.UnloadSceneAsync((int)SceneIndexes.GAME);
		SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
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

		//Once loading is done
		Living player = FindObjectOfType<Player>().Living;
		camera.SetTarget(player.transform);
		if (GameData.Load()) {
			Debug.Log("Load successful!");
			player.LoadData(GameData.current.player);
			paused = false;
		} else {
			Debug.LogError("Load failed! Starting a new game...");
		}

		loadingScreen.gameObject.SetActive(false);
	}
}

public enum SceneIndexes {
	MANAGER = 0,
	MAIN_MENU = 1,
	GAME = 2
}