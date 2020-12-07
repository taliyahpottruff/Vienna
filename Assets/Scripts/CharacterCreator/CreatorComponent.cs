using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Vienna.CharacterCreator {
    public class CreatorComponent : MonoBehaviour {
        public string type;
        public TMP_Dropdown dropdown;

        public Image componentImage;

        public void DropdownChanged(int value) {
            var name = dropdown.options[value].text;
            var sprite = Resources.Load<Sprite>($"Sprites/{type}/{name}");
            componentImage.sprite = sprite;

            switch(type) {
                case "Hair":
                    CharacterCreator.instance.hairType = name;
                    break;
                case "Tops":
                    CharacterCreator.instance.topType = name;
                    break;
                case "Bottoms":
                    CharacterCreator.instance.bottomType = name;
                    break;
            }
        }

        public void RedChanged(float value) {
            componentImage.color = new Color(value, componentImage.color.g, componentImage.color.b);
        }

        public void GreenChanged(float value) {
            componentImage.color = new Color(componentImage.color.r, value, componentImage.color.b);
        }

        public void BlueChanged(float value) {
            componentImage.color = new Color(componentImage.color.r, componentImage.color.g, value);
        }
    }
}