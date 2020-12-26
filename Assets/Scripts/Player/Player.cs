using UnityEngine;
using Vienna.Data;

/*
 * AUTHOR: Trenton Pottruff
 */

namespace Vienna {
	public class Player : Living {
		private UIManager uiManager;
		private Controls controls;

		private void Awake() {
			uiManager = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<UIManager>();
			controls = new Controls();
			controls.UI.Inventory.performed += Inventory_performed;
			controls.Player.Interact.performed += Interact_performed;
		}

        public override void DealDamage(float damage) {
            base.DealDamage(damage);
			HealthBar.instance.UpdateHealthBar();
		}

        public override void AddHealth(float health) {
            base.AddHealth(health);
			HealthBar.instance.UpdateHealthBar();
		}

        public override void LoadData(LivingData data) {
            base.LoadData(data);
			HealthBar.instance.UpdateHealthBar();
			HealthBar.instance.UpdateEffectImages();
		}

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Entities"));
			if (hit) {
				IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
				if (interactable != null) {
					object interacted = interactable.Interact();
					if (interacted.GetType() == typeof(Inventory)) {
						//Display inventory being interacted with
						uiManager.ObserveInventory((Inventory)interacted);
					}
				}
			}
		}

		private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
			if (uiManager.CanToggleInventory()) {
				GameManager.singleton.Paused = uiManager.ToggleInventory();
			}
		}

		private void OnEnable() {
			controls.Enable();
		}

		private void OnDisable() {
			controls.Disable();
		}

        protected override void ExtraEffectProcessing() {
			HealthBar.instance.UpdateEffectImages();
        }
    }
}