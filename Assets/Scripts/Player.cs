using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Player : MonoBehaviour {
	public bool paused = false;

	private UIManager uiManager;

	private void Awake() {
		uiManager = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<UIManager>();
	}

	private void Update() {
		if (Input.ButtonPress("Pause")) {
			paused = !paused;
			uiManager.TogglePauseScreen(paused);
		}
	}
}