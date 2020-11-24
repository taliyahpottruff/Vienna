﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vienna.Items {
	[Serializable]
	public class MedicalItem : IBaseItem, IConsumable, IStackable {
		public string Name { get; set; }
		public string Sprite { get; set; }
		public int stack { get; set; }
		public int maxStack { get; set; }

		// Properties
		public HealthEffect healthEffect = new HealthEffect() { secondsRemaining = 30, effects = new Dictionary<string, object>() { { "healing", 5f } } };

		public MedicalItem(string name) : this(name, name, 1) { }

		public MedicalItem(string name, string sprite) : this(name, sprite, 1) { }

		public MedicalItem(string name, string sprite, int amount) : this(name, sprite, amount, 64) { }

		public MedicalItem(string name, string sprite, int amount, int maxStack) {
			this.Name = name;
			this.Sprite = sprite;
			stack = amount;
			this.maxStack = maxStack;
		}

		public int GetAmount() {
			return stack;
		}

		public int Consume(Living user, int amount) {
			//TODO: Add health logic
			user.healthEffects.Add(healthEffect);
			Debug.Log($"{user.firstName} is healing themselves with \"{Name}\"");
			return Remove(amount);
		}

		public int Remove(int amount) {
			stack = Mathf.Max(stack - amount, 0);
			return stack;
		}

		public int Use(Living user) {
			return Consume(user, 1);
		}
	}
}