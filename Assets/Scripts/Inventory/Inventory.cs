using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vienna.Items;

namespace Vienna {
	public class Inventory : MonoBehaviour {
		[SerializeField]
		private List<IBaseItem> items = new List<IBaseItem>();

		public List<IBaseItem> Items => items;

		private Living entity;

		#region Events
		public delegate void ChangeEvent();
		public event ChangeEvent OnChange;
		private void CallChangeEvent() => OnChange?.Invoke();
		#endregion

		private void Awake() {
			entity = GetComponent<Living>();
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
			if (items[index].Use(entity) <= 0) {
				//Remove item from inventory
				items.RemoveAt(index);
			}
			CallChangeEvent();
		}

		public IBaseItem RemoveItem(int index) {
			IBaseItem item = items[index];
			items.RemoveAt(index);
			CallChangeEvent();
			return item;
		}
	}
}