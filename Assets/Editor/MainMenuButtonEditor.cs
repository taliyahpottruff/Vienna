using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vienna {
	[CustomEditor(typeof(MainMenuButton))]
	public class MainMenuButtonEditor : Editor {
		MainMenuButton button;

		private void OnEnable() {
			button = (MainMenuButton)target;
		}

		public override void OnInspectorGUI() {
			EditorGUILayout.PropertyField(serializedObject.FindProperty("normal"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("highlighted"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_OnClick"));
			serializedObject.ApplyModifiedProperties();
		}
	}
}
