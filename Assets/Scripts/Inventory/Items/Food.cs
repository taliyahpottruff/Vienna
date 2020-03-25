using System;
using UnityEngine;

[Serializable]
public class Food : IBaseItem, IConsumable, IStackable {
	public string name { get; set; }
	public string sprite { get; set; }
	public int stack { get; set; } = 1;
	public int maxStack { get; set; } = 64;

	public int Consume(int amount) {
		//TODO: Do something with food
		return Remove(amount);
	}

	public int Remove(int amount) {
		stack = Mathf.Max(stack - amount, 0);
		return stack;
	}
}