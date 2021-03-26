using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
    [CreateAssetMenu(menuName = "Weapon")]
    public class Weapon : ScriptableObject {
        public Type type;
        public int cost, dmgDice_amount, dmgDice_type, weight;
        public DamageType dmgType;
        public bool twoHanded;

        public enum Type {
            Melee, Ranged, Magic
        }
    }
}