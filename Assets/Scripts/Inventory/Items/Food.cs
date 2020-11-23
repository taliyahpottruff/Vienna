using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Vienna.Items {
	[Serializable]
	public class Food : IBaseItem, IConsumable, IStackable {
		public string Name { get; set; }
		public string Sprite { get; set; }
		public int stack { get; set; }
		public int maxStack { get; set; }

		public Food(string name) : this(name, name, 1) { }

		public Food(string name, string sprite) : this(name, sprite, 1) { }

		public Food(string name, string sprite, int amount) : this(name, sprite, amount, 64) { }

		public Food(string name, string sprite, int amount, int maxStack) {
			this.Name = name;
			this.Sprite = sprite;
			stack = amount;
			this.maxStack = maxStack;
		}

		public int GetAmount() {
			return stack;
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
			stack = Mathf.Max(stack - amount, 0);
			return stack;
		}
	}
}