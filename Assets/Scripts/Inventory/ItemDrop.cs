using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class ItemDrop : MonoBehaviour {
	public string type;

	[SerializeField]
	private IBaseItem item;

	private SpriteRenderer sr;

	private void Awake() {
		//Get required components
		sr = GetComponent<SpriteRenderer>();

		//TODO: Remove this
		switch (type) {
			case "Bandaid":
				Initialize(new MedicalItem("Bandaid"));
				break;
			default:
				Initialize(new Food("Egg"));
				break;
		}
	}

	private void Start() {
		//Initialize item
		sr.sprite = Resources.Load<Sprite>($"Sprites/Items/{item.sprite}");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Inventory inventory = collision.GetComponent<Inventory>();
		if (inventory != null) {
			Debug.Log("Picking up...");
			inventory.Add(item);
			Destroy(gameObject);
		} else {
			Debug.LogWarning("Can't pickup this object. The entity doesn't have an inventory!");
		}
	}

	public void Initialize(IBaseItem item) {
		this.item = item;
	}
}