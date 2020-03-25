using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class Food : IBaseItem, IConsumable, IStackable {
	public string name { get; set; }
	public string sprite { get; set; }
	public int stack { get; set; }
	public int maxStack { get; set; }

	public Food(string name) : this(name, name, 1) {}

	public Food(string name, string sprite) : this(name, sprite, 1) {}

	public Food(string name, string sprite, int amount) : this(name, sprite, amount, 64) {}

	public Food(string name, string sprite, int amount, int maxStack) {
		this.name = name;
		this.sprite = sprite;
		stack = amount;
		this.maxStack = maxStack;
	}

	public int Use() {
		return Consume(1);
	}

	public int Consume(int amount) {
		//TODO: Do something with food
		Debug.Log($"Player ate the {name}!");
		return Remove(amount);
	}

	public int Remove(int amount) {
		stack = Mathf.Max(stack - amount, 0);
		return stack;
	}
}