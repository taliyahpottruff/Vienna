using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class UIManager : MonoBehaviour {
	[SerializeField]
	GameObject vitals, pauseScreen, inventory;

	public void SetPauseScreen(bool newPaused) {
		vitals.SetActive(!newPaused);
		pauseScreen.SetActive(newPaused);
	}

	public bool ToggleInventory() {
		bool currentStatus = inventory.activeSelf;
		vitals.SetActive(currentStatus);
		inventory.SetActive(!currentStatus);
		return inventory.activeSelf;
	}

	public bool PausedElsewhere() => (GameManager.singleton.paused && !pauseScreen.activeSelf);
	public bool CanToggleInventory() => (GameManager.singleton.paused && inventory.activeSelf) || (!GameManager.singleton.paused && !inventory.activeSelf);
}