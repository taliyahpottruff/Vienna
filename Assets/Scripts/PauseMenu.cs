﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour{
	public void SaveGame() {
		Living player = FindObjectOfType<Player>().Living;
		GameData.Save(player);
	}

	public void LoadGame() {
		Living player = FindObjectOfType<Player>().Living;
		if (GameData.Load()) {
			Debug.Log("Load successful!");
			player.LoadData(GameData.current.player);
		} else {
			Debug.LogError("Load failed!");
		}
	}

	public void ShowSettings() {
		Debug.LogError("Settings are not implemented in-game yet!");
	}

	public void QuitToMenu() {
		GameManager.singleton.GameToMenu();
	}
}