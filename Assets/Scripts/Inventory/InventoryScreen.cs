using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vienna.Items;

namespace Vienna {
	public class InventoryScreen : MonoBehaviour {
		public Inventory inventory, observed;
		public Transform content, observedContent;
		public InventoryScreenItem equippedWeapon;

		[SerializeField]
		private GameObject displayItemPrefab;

		private List<GameObject> displayItems = new List<GameObject>(), observedItems = new List<GameObject>();

		private void Awake() {
			inventory.OnChange += UpdateUI;
		}

		public void Observe(Inventory inv) {
			observed = inv;
			observed.OnChange += UpdateObservedUI;
			UpdateUI();
			UpdateObservedUI();
		}

		public void Unobserve() {
			if (observed != null) {
				observed.OnChange -= UpdateObservedUI;
				observed = null;
			}
			UpdateUI();
		}

		public void ClearDisplay(List<GameObject> displayItems) {
			foreach (GameObject obj in displayItems) {
				Destroy(obj);
			}
			equippedWeapon.Clear();
		}

		private void UpdateUI() {
			//Clear display then redraw
			ClearDisplay(displayItems);
			for (int i = 0; i < inventory.Items.Count; i++) {
				var item = inventory.Items[i];
				if (item is IEquippable) {
					if ((item as IEquippable).Equipped) {
						if (item is IWeapon) { // If item is Equipped Weapon
							equippedWeapon.Initialize(inventory, i);
                        }

						continue;
                    }
                }

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
}