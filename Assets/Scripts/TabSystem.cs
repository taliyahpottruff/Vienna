using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSystem : MonoBehaviour {
	[SerializeField]
	private List<GameObject> pages = new List<GameObject>();

	private void Start() {
		SwitchTo(0);
	}

	public void SwitchTo(int index) {
		for (int i = 0; i < pages.Count; i++) {
			pages[i].SetActive(i == index);
		}
	}
}