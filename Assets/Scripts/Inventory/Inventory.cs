using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	[SerializeField]
	private List<IBaseItem> items = new List<IBaseItem>();

	Controls controls;

	private void Awake() {
		controls = new Controls();
		controls.Player.Interact.performed += Interact_performed;
	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		UseItem(0);
	}

	private void OnEnable() {
		controls.Enable();
	}

	private void OnDisable() {
		controls.Disable();
	}

	public void Add(IBaseItem item) {
		//TODO: Stack if possible
		items.Add(item);
	}

	public void UseItem(int index) {
		if (items[index].Use() <= 0) {
			//Remove item from inventory
			items.RemoveAt(index);
		}
	}
}