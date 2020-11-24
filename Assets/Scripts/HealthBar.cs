using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vienna {
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour {
        public static HealthBar instance;

        public Living entity;
        public Transform effectBar;
        public GameObject effectImage;
        public TextMeshProUGUI displayText;

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
            displayText.text = $"{entity.health}/{entity.maxHealth}";
        }

        public void UpdateEffectImages() {
            for (int i = 0; i < effectBar.childCount; i++) {
                Destroy(effectBar.GetChild(i).gameObject);
            }

            foreach (var effect in entity.healthEffects) {
                GameObject obj = Instantiate<GameObject>(effectImage, effectBar);
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/{effect.image}");
            }
        }
    }
}