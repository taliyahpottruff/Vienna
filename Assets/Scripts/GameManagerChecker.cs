using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerChecker : MonoBehaviour {
	private void Awake() {
		int sceneCount = SceneManager.sceneCount;
		List<Scene> scenes = new List<Scene>();
		bool persistentSceneLoaded = false;

		for (int i = 0; i < sceneCount; i++) {
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.buildIndex == 0) {
				Debug.Log("Persistent scene is loaded");
				persistentSceneLoaded = true;
				continue;
			}
			scenes.Add(scene);
		}

		if (!persistentSceneLoaded) {
			Debug.LogWarning("Persistent scene is not loaded, reloading...");
			SceneManager.LoadScene((int)SceneIndexes.MANAGER, LoadSceneMode.Additive);
		} else {
			Destroy(gameObject);
		}

	}
}