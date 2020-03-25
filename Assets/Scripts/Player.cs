using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Player : MonoBehaviour {
	private UIManager uiManager;
	private Controls controls;

	public Living Living {
		get {
			return GetComponent<Living>();
		}
	}

	private void Awake() {
		uiManager = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<UIManager>();
		controls = new Controls();
		controls.UI.Pause.performed += Pause_performed;
		controls.UI.Inventory.performed += Inventory_performed;
	}

	private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		if (uiManager.CanToggleInventory()) {
			GameManager.singleton.paused = uiManager.ToggleInventory();
		}
	}

	private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		if (!uiManager.PausedElsewhere()) {
			GameManager.singleton.paused = !GameManager.singleton.paused;
			uiManager.SetPauseScreen(GameManager.singleton.paused);
		} else {
			if (uiManager.CanToggleInventory()) GameManager.singleton.paused = uiManager.ToggleInventory();
		}
	}

	private void OnEnable() {
		controls.Enable();
	}

	private void OnDisable() {
		controls.Disable();
	}
}