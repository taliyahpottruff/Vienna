using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vienna.Data;

namespace Vienna.CharacterCreator {
    public class CharacterCreator : MonoBehaviour {
        public Image hair, top, bottom;

        public string hairType, topType, bottomType;

        public static CharacterCreator instance;

        private void Start() {
            instance = this;
        }

        public void StartGame() {
            GameData.Clear();
            GameData.SetPlayerData(new LivingData() {
                firstName = "Player",
                lastName = "Lastname",
                hairType = hairType,
                topType = topType,
                bottomType = bottomType,
                hairColorR = hair.color.r,
                hairColorG = hair.color.g,
                hairColorB = hair.color.b,
                topColorR = top.color.r,
                topColorG = top.color.g,
                topColorB = top.color.b,
                bottomColorR = bottom.color.r,
                bottomColorG = bottom.color.g,
                bottomColorB = bottom.color.b,
                health = 100,
                maxHealth = 100,
                species = Species.Human
            });
            GameManager.singleton.LoadGame(true);
        }

        public void SetHairType(string type) {
            hairType = type;
        }

        public void SetTopType(string type) {
            topType = type;
        }

        public void SetBottomType(string type) {
            bottomType = type;
        }
    }
}