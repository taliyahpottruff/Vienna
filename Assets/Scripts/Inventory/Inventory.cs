using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	[SerializeField]
	private List<IBaseItem> items = new List<IBaseItem>();

	private Controls controls;

	public List<IBaseItem> Items => items;

	#region Events
	public delegate void ChangeEvent();
	public event ChangeEvent onChange;
	private void CallChangeEvent() => onChange?.Invoke();
	#endregion

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
		CallChangeEvent();
	}

	public void SetItems(List<IBaseItem> items) {
		this.items.Clear();
		if (items != null) {
			foreach (IBaseItem item in items) {
				this.items.Add(item);
			}
		}
		CallChangeEvent();
	}

	public void UseItem(int index) {
		if (items[index].Use() <= 0) {
			//Remove item from inventory
			items.RemoveAt(index);
		}
		CallChangeEvent();
	}
}