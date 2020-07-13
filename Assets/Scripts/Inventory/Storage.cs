using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Inventory))]
public class Storage : MonoBehaviour, IInteractable {
	private Inventory inventory;

	private void Awake() {
		inventory = GetComponent<Inventory>();
		inventory.Add(new MedicalItem("Bandaid", "Egg", 24));
	}

	public object Interact() {
		return inventory;
	}
}