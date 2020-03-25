using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour {
	[SerializeField]
	private ItemData item;

	private SpriteRenderer sr;

	private void Awake() {
		//Get required components
		sr = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		//Initialize item
		sr.sprite = Resources.Load<Sprite>($"Sprites/Items/{item.sprite}");
	}
}

[Serializable]
public class ItemData {
	public string name;
	public string sprite;
	public int stack;
	public int maxStack = 64;
}