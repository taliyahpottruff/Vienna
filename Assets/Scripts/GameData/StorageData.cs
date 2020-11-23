using System;
using UnityEngine;
using Vienna.Items;

namespace Vienna.Data {
	[Serializable]
	public class StorageData {
		public string type;
		public Vector2 position;
		public IBaseItem[] items;
	}
}