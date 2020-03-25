using System;
using UnityEngine;

[Serializable]
public class MedicalItem : IBaseItem, IConsumable, IStackable {
	public string name { get; set; }
	public string sprite { get; set; }
	public int stack { get; set; }
	public int maxStack { get; set; }

	public MedicalItem(string name) : this(name, name, 1) {}

	public MedicalItem(string name, string sprite) : this(name, sprite, 1) {}

	public MedicalItem(string name, string sprite, int amount) : this(name, sprite, amount, 64) {}

	public MedicalItem(string name, string sprite, int amount, int maxStack) {
		this.name = name;
		this.sprite = sprite;
		stack = amount;
		this.maxStack = maxStack;
	}

	public int Consume(int amount) {
		//TODO: Add logic
		Debug.Log($"Player is healing themselves with \"{name}\"");
		return Remove(amount);
	}

	public int Remove(int amount) {
		stack = Mathf.Max(stack - amount, 0);
		return stack;
	}

	public int Use() {
		return Consume(1);
	}
}