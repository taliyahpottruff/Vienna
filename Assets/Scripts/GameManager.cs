using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	private GameObject loadingScreen;

	private void Awake() {
		instance = this;

		if (SceneManager.sceneCount < 2) {
			SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive);
		}
	}

	public void LoadGame() {
		loadingScreen.SetActive(true);
		SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU);
		SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive);
	}
}

public enum SceneIndexes {
	MANAGER = 0,
	MAIN_MENU = 1,
	GAME = 2
}