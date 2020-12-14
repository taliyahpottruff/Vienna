using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace Vienna.Items {
	public class InventoryScreenItem : MonoBehaviour {
		public int inventoryIndex = -1;
		public Inventory inventory, transferTo;

		[SerializeField]
		private Image image;
		[SerializeField]
		private TextMeshProUGUI title;

		public void Clear() {
			inventoryIndex = -1;
			image.sprite = null;
			title.text = "Weapon";
        }

		public void Initialize(Inventory inventory, int index) {
			//Set values
			this.inventory = inventory;
			inventoryIndex = index;

			//Setup the display
			IBaseItem item = inventory.Items[inventoryIndex];
			image.sprite = Resources.Load<Sprite>($"Sprites/Items/{item.Sprite}");
			title.text = $"{item.Name} x{item.GetAmount()}";
		}

		public void Initialize(Inventory inventory, int index, Inventory other) {
			Initialize(inventory, index);
			transferTo = other;
		}

		public void Use() {
			if (inventoryIndex >= 0) {
				if (transferTo == null) {
					//Use the item if no transfer
					inventory.UseItem(inventoryIndex);
				} else {
					//If a transfer is available, do that
					transferTo.Add(inventory.RemoveItem(inventoryIndex));
				}
			}
		}
	}
}