using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {
	public Inventory inventory, observed;
	public Transform content, observedContent;

	[SerializeField]
	private GameObject displayItemPrefab;

	private List<GameObject> displayItems = new List<GameObject>(), observedItems = new List<GameObject>();

	private void Awake() {
		inventory.onChange += UpdateUI;
	}

	public void Observe(Inventory inv) {
		observed = inv;
		observed.onChange += UpdateObservedUI;
		UpdateUI();
		UpdateObservedUI();
	}

	public void Unobserve() {
		if (observed != null) {
			observed.onChange -= UpdateObservedUI;
			observed = null;
		}
		UpdateUI();
	}

	public void ClearDisplay(List<GameObject> displayItems) {
		foreach (GameObject obj in displayItems) {
			Destroy(obj);
		}
	}

	private void UpdateUI() {
		//Clear display then redraw
		ClearDisplay(displayItems);
		for (int i = 0; i < inventory.Items.Count; i++) {
			GameObject obj = Instantiate<GameObject>(displayItemPrefab, content);
			InventoryScreenItem displayItem = obj.GetComponent<InventoryScreenItem>();
			displayItem.Initialize(inventory, i, observed);
			displayItems.Add(obj);
		}
	}

	private void UpdateObservedUI() {
		//Clear display then redraw
		ClearDisplay(observedItems);
		for (int i = 0; i < observed.Items.Count; i++) {
			GameObject obj = Instantiate<GameObject>(displayItemPrefab, observedContent);
			InventoryScreenItem displayItem = obj.GetComponent<InventoryScreenItem>();
			displayItem.Initialize(observed, i, inventory);
			observedItems.Add(obj);
		}
	}
}