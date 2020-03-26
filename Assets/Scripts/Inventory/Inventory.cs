using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	[SerializeField]
	private List<IBaseItem> items = new List<IBaseItem>();

	public List<IBaseItem> Items => items;

	#region Events
	public delegate void ChangeEvent();
	public event ChangeEvent onChange;
	private void CallChangeEvent() => onChange?.Invoke();
	#endregion

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