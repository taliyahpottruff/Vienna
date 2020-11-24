using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vienna {
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour {
        public static HealthBar instance;

        public Living entity;

        Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();

            instance = this;
        }

        private void Start() {
            UpdateHealthBar();
        }

        private void OnDestroy() {
            instance = null;
        }

        public void UpdateHealthBar() {
            slider.value = entity.health / entity.maxHealth;
        }
    }
}