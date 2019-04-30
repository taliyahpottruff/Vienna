using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public static class Input {
	public static Dictionary<string, KeyCode> KEYBOARD_CONTROLS = new Dictionary<string, KeyCode>() {
		{"Move_Up", KeyCode.W},
		{"Move_Down", KeyCode.S},
		{"Move_Left", KeyCode.A},
		{"Move_Right", KeyCode.D},
		{"Pause", KeyCode.Escape}
	};

	private static ControlType type = ControlType.Keyboard;
	public static ControlType TYPE {
		get { return type; }
		set { type = value; }
	}

	public static bool ButtonPress(string button) {
		if (type == ControlType.Keyboard) {
			//If corresponding key is pressed, return true
			if (UnityEngine.Input.GetKeyDown(KEYBOARD_CONTROLS[button])) {
				return true;
			}
		}

		return false;
	}

	public static bool ButtonHold(string button) {
		if (type == ControlType.Keyboard) {
			//If corresponding key is pressed, return true
			if (UnityEngine.Input.GetKey(KEYBOARD_CONTROLS[button])) {
				return true;
			}
		}

		return false;
	}

	#region Movement Axes
	public static Vector2 GetMovementVector() {
		if (type == ControlType.Keyboard) {
			return new Vector2(
				((ButtonHold("Move_Right")) ? 1 : 0) - ((ButtonHold("Move_Left")) ? 1 : 0),
				((ButtonHold("Move_Up")) ? 1 : 0) - ((ButtonHold("Move_Down")) ? 1 : 0)
			);
		}

		return Vector2.zero;
	}
	#endregion
}

public enum ControlType {
	Keyboard, Controller
}