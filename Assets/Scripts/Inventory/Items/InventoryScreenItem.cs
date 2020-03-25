using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventoryScreenItem : MonoBehaviour {
	public int inventoryIndex;
	public Inventory inventory;

	[SerializeField]
	private Image image;
	[SerializeField]
	private TextMeshProUGUI title;

	public void Initialize(Inventory inventory, int index) {
		//Set values
		this.inventory = inventory;
		inventoryIndex = index;

		//Setup the display
		IBaseItem item = inventory.Items[inventoryIndex];
		image.sprite = Resources.Load<Sprite>($"Sprites/Items/{item.sprite}");
		title.text = $"{item.name} x{item.GetAmount()}";
	}

	public void Use() {
		inventory.UseItem(inventoryIndex);
	}
}