using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class InputDetection : MonoBehaviour {
	private void Update() {
		//If keyboard detected
		if (UnityEngine.Input.anyKeyDown) {
			Input.TYPE = ControlType.Keyboard;
		}
		//TODO: If controller detected
	}
}