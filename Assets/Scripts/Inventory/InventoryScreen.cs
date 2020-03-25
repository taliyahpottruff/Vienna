using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {
	public Inventory inventory;
	public Transform content;

	[SerializeField]
	private GameObject displayItemPrefab;

	private List<GameObject> displayItems = new List<GameObject>();

	private void Awake() {
		inventory.onChange += UpdateUI;
	}

	public void ClearDisplay() {
		foreach (GameObject obj in displayItems) {
			Destroy(obj);
		}
	}

	private void UpdateUI() {
		//Clear display then redraw
		ClearDisplay();
		for (int i = 0; i < inventory.Items.Count; i++) {
			GameObject obj = Instantiate<GameObject>(displayItemPrefab, content);
			InventoryScreenItem displayItem = obj.GetComponent<InventoryScreenItem>();
			displayItem.Initialize(inventory, i);
			displayItems.Add(obj);
		}
	}
}