using System;
using System.Collections.Generic;
using UnityEngine;
using Vienna.Items;

namespace Vienna.Data {
	[Serializable]
	public class LivingData {
		public Species species;
		public string firstName;
		public string lastName;
		public string hairType, topType, bottomType;
		public float skinColorR, skinColorG, skinColorB;
		public float hairColorR, hairColorG, hairColorB;
		public float topColorR, topColorG, topColorB;
		public float bottomColorR, bottomColorG, bottomColorB;
		public Vector2 position;
		public float health;
		public float maxHealth;
		public List<IBaseItem> inventory;
		public List<HealthEffect> healthEffects;
	}
}