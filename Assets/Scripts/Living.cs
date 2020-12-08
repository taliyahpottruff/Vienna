using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vienna.Data;
using Vienna.Items;

namespace Vienna {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LivingAnimator))]
    public abstract class Living : MonoBehaviour {
        public Species species = Species.Human;
        public string firstName;
        public string lastName;
        public string hairType = "Short Hair", topType = "T-Shirt", bottomType = "Pants";
        public float health, maxHealth;
        private float healingMultiplier = 1f;
        private float healthMultiplier = 1f;
        public List<HealthEffect> healthEffects = new List<HealthEffect>();

        private Coroutine regenCoroutine;
        private int effectLength = 0;

        public LivingAnimator animator;

        private void Start() {
            animator = GetComponent<LivingAnimator>();

            regenCoroutine = StartCoroutine(RegenerateHealth(false));
            StartCoroutine(ProcessEffects());
        }

        public virtual void AddHealth(float health) {
            this.health = Mathf.Min(this.health + health, maxHealth);
        }

        public virtual void DealDamage(float damage) {
            health -= damage;
            StopCoroutine(regenCoroutine);
            regenCoroutine = StartCoroutine(RegenerateHealth());

            if (health < 0) {
                Debug.LogError("DEAD");
            }
        }

        public virtual void LoadData(LivingData data) {
            // First, set the data
            species = data.species;
            firstName = data.firstName;
            lastName = data.lastName;
            health = data.health;
            maxHealth = data.maxHealth;
            hairType = data.hairType;
            topType = data.topType;
            bottomType = data.bottomType;
            GetComponent<SpriteRenderer>().color = new Color(data.skinColorR, data.skinColorG, data.skinColorB);
            animator.hairRenderer.color = new Color(data.hairColorR, data.hairColorG, data.hairColorB);
            animator.topRenderer.color = new Color(data.topColorR, data.topColorG, data.topColorB);
            animator.bottomRenderer.color = new Color(data.bottomColorR, data.bottomColorG, data.bottomColorB);
            healthEffects = (data.healthEffects != null) ? data.healthEffects : new List<HealthEffect>();
            Inventory inventory = GetComponent<Inventory>();
            if (inventory != null) inventory.SetItems(data.inventory);

            // Then set other stuff
            transform.position = data.position;

            // Load spirte
            animator = GetComponent<LivingAnimator>();
            animator.LoadSprites();
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
                AddHealth(1 * healthMultiplier);
            }
        }

        private IEnumerator ProcessEffects() {
            while (true) {
                if (effectLength != healthEffects.Count) {
                    effectLength = healthEffects.Count;
                    ExtraEffectProcessing();
                }

                float largestHealthMultiplier = 1f;
                float healthMultiplier = 1f;
                int effectIndex = 0;
                List<int> removalIndexes = new List<int>();
                int lastBleedingEffect = -1;
                foreach (var effect in healthEffects) {
                    if (effect.effects.ContainsKey("healing")) {
                        largestHealthMultiplier = Mathf.Max(largestHealthMultiplier, (float)effect.effects["healing"]);

                        if (healthMultiplier < 0 && lastBleedingEffect >= 0) {
                            this.healthMultiplier = 1;
                            removalIndexes.Add(lastBleedingEffect);
                            lastBleedingEffect = -1;
                        }
                    }
                    else if (effect.effects.ContainsKey("bleeding")) {
                        largestHealthMultiplier = 1;
                        healthMultiplier = (float) effect.effects["bleeding"];
                        lastBleedingEffect = effectIndex;
                    }

                    effect.secondsRemaining--;
                    if (effect.secondsRemaining <= 0) removalIndexes.Add(effectIndex);
                    effectIndex++;
                }

                // Remove all finished effects
                for (int i = removalIndexes.Count - 1; i >= 0; i--) {
                    healthEffects.RemoveAt(removalIndexes[i]);
                    ExtraEffectProcessing();
                }

                // Apply effects
                if (healingMultiplier != largestHealthMultiplier) {
                    StopCoroutine(regenCoroutine);
                    healingMultiplier = largestHealthMultiplier;
                    regenCoroutine = StartCoroutine(RegenerateHealth(false));
                }
                if (healthMultiplier != this.healthMultiplier) {
                    this.healthMultiplier = healthMultiplier;
                }

                yield return new WaitForSeconds(1);
            }
        }

        protected abstract void ExtraEffectProcessing();
    }

    public enum Species {
        Human
    }
}