using UnityEngine;

namespace Vienna {
	public class MainMenu : MonoBehaviour {
		public void StartGame() {
			GameManager.singleton.LoadGame();
		}

		public void QuitGame() {
			GameManager.singleton.QuitGame();
		}
	}
}