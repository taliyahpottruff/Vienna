using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	[SerializeField]
	private List<Food> items = new List<Food>();

	public void Add(Food item) {
		//TODO: Stack if possible
		items.Add(item);
	}
}