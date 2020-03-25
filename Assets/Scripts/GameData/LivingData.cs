using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LivingData {
	public Species species;
	public string firstName;
	public string lastName;
	public Vector2 position;
	public float health;
	public float maxHealth;
	public List<IBaseItem> inventory;
}