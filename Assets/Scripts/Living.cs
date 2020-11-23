using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vienna.Data;
using Vienna.Items;

namespace Vienna {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Living : MonoBehaviour {
		public Species species = Species.Human;
		public string firstName;
		public string lastName;
		public float health, maxHealth;
		private float healingMultiplier = 1f;
		public List<HealthEffect> healthEffects = new List<HealthEffect>();

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
			StartCoroutine(ProcessEffects());
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
				yield return new WaitForSeconds(5f / healingMultiplier);
				AddHealth(1);
			}
		}

		private IEnumerator ProcessEffects() {
			while (true) {
				float largestHealthMultiplier = 1f;
				int effectIndex = 0;
				List<int> removalIndexes = new List<int>();
                foreach (var effect in healthEffects) {
					if (effect.effects.ContainsKey("healing")) largestHealthMultiplier = Mathf.Max(largestHealthMultiplier, (float) effect.effects["healing"]);

					effect.secondsRemaining--;
					if (effect.secondsRemaining <= 0) removalIndexes.Add(effectIndex);
					effectIndex++;
                }

                // Remove all finished effects
                foreach (var index in removalIndexes) {
					healthEffects.RemoveAt(index);
                }

				// Apply effects
				if (healingMultiplier != largestHealthMultiplier) {
					StopCoroutine(regenCoroutine);
					healingMultiplier = largestHealthMultiplier;
					regenCoroutine = StartCoroutine(RegenerateHealth(false));
                }

				yield return new WaitForSeconds(1);
            }
        }
	}

	public enum Species {
		Human
	}
}