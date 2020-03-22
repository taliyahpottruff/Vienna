using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainMenuButton))]
[CanEditMultipleObjects]
public class MainMenuButtonEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
	}
}
