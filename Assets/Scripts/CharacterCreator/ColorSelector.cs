using UnityEngine;
using UnityEngine.UI;

namespace Vienna.CharacterCreator {
    [RequireComponent(typeof(Image))]
    public class ColorSelector : MonoBehaviour {
        public Image target;

        private Image image;

        private void Start() {
            image = GetComponent<Image>();
        }

        public void ChangeColor() {
            target.color = image.color;
        }
    }
}