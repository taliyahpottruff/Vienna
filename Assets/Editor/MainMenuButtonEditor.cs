using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainMenuButton))]
public class MainMenuButtonEditor : Editor {
	MainMenuButton button;

	private void OnEnable() {
		button = (MainMenuButton) target;
	}

	public override void OnInspectorGUI() {
		EditorGUILayout.PropertyField(serializedObject.FindProperty("normal"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("highlighted"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("clickEvent"));

		if (button.onClick.Equals(button.clickEvent)) {
			button.onClick = button.clickEvent;
		}
		serializedObject.ApplyModifiedProperties();
	}
}
