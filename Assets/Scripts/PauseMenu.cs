using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vienna.Data;

namespace Vienna {
	public class PauseMenu : MonoBehaviour {
		[SerializeField]
		UIManager uiManager;

		public void SaveGame() {
			Living player = FindObjectOfType<Player>();
			if (GameData.Save(player)) {
				GameManager.singleton.paused = false;
				uiManager.SetPauseScreen(false);
			} else {
				Debug.LogError("There was an error saving!");
			}
		}

		public void LoadGame() {
			GameManager.singleton.LoadGame();
		}

		public void ShowSettings() {
			Debug.LogError("Settings are not implemented in-game yet!");
		}

		public void QuitToMenu() {
			GameManager.singleton.GameToMenu();
		}
	}
}