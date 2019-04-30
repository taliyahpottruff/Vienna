using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class UIManager : MonoBehaviour {
	[SerializeField]
	GameObject vitals, pauseScreen;

	public void TogglePauseScreen(bool newPaused) {
		vitals.SetActive(!newPaused);
		pauseScreen.SetActive(newPaused);
	}
}