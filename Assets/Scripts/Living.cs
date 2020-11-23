using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Living : MonoBehaviour {
    public Species species = Species.Human;
    public string firstName;
    public string lastName;
	public float health, maxHealth;

	#region Displays
	private Slider healthSlider;
	#endregion

	private Coroutine regenCoroutine;

	private void Awake() {
		healthSlider = GameObject.FindGameObjectWithTag("Health Bar").GetComponent<Slider>();

		healthSlider.maxValue = maxHealth;
	}

    private void Start() {
		regenCoroutine = StartCoroutine(RegenerateHealth(false));
    }

    private void Update() {
		healthSlider.value = health;
	}

	public void AddHealth(float health) {
		this.health = Mathf.Min(this.health + health, maxHealth);
    }

	public void DealDamage(float damage) {
		health -= damage;
		StopCoroutine(regenCoroutine);
		regenCoroutine = StartCoroutine(RegenerateHealth());

		if (health < 0) {
			Debug.LogError("DEAD");
        }
    }

	public void LoadData(LivingData data) {
		//First, set the data
		species = data.species;
		firstName = data.firstName;
		lastName = data.lastName;
		health = data.health;
		maxHealth = data.maxHealth;
		Inventory inventory = GetComponent<Inventory>();
		if (inventory != null) inventory.SetItems(data.inventory);

		//Then set other stuff
		transform.position = data.position;
	}

	public List<IBaseItem> GetInventoryItems() {
		return GetComponent<Inventory>().Items;
	}

	private IEnumerator RegenerateHealth(bool cooldown = true) {
		if (cooldown) {
			yield return new WaitForSeconds(30);
        }

		while (true) {
			AddHealth(1);
			yield return new WaitForSeconds(5);
        }
    }
}

public enum Species {
    Human
}