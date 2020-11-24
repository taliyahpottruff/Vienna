using UnityEngine;
using System.Collections;
using Vienna.Items;

namespace Vienna {
	[RequireComponent(typeof(Inventory))]
	public class Storage : MonoBehaviour, IInteractable {
		private Inventory inventory;

		private void Awake() {
			inventory = GetComponent<Inventory>();
			inventory.Add(new MedicalItem("Bandage", "Egg", 24));
		}

		public object Interact() {
			return inventory;
		}
	}
}