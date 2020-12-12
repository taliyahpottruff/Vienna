using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Vienna.Items {
	[Serializable]
	public class Food : IBaseItem, IConsumable, IStackable {
		public string Name { get; set; }
		public string Sprite { get; set; }
		public int Stack { get; set; }
		public int MaxStack { get; set; }

		public Food(string name) : this(name, name, 1) { }

		public Food(string name, string sprite) : this(name, sprite, 1) { }

		public Food(string name, string sprite, int amount) : this(name, sprite, amount, 64) { }

		public Food(string name, string sprite, int amount, int maxStack) {
			this.Name = name;
			this.Sprite = sprite;
			Stack = amount;
			this.MaxStack = maxStack;
		}

		public int GetAmount() {
			return Stack;
		}

		public int Use(Living user) {
			return Consume(user, 1);
		}

		public int Consume(Living user, int amount) {
			//TODO: Do something with food
			Debug.Log($"Player ate the {Name}!");
			return Remove(amount);
		}

		public int Remove(int amount) {
			Stack = Mathf.Max(Stack - amount, 0);
			return Stack;
		}
	}
}