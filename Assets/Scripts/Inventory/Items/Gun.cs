using System;

namespace Vienna.Items {
    [Serializable]
    public class Gun : IBaseItem, IWeapon, IEquippable {
        public string Name { get => name; set => name = value; }
        public string Sprite { get => sprite; set => sprite = value; }
        public bool Equipped { get => equipped; set => equipped = value; }

        private string name, sprite;
        private bool equipped;

        public float Attack(float attack) {
            throw new System.NotImplementedException();
        }

        public int GetAmount() {
            return 1;
        }

        public int Use(Living user) {
            // Equip the weapon
            Equipped = user.EquipWeapon(this);
            return 1;
        }
    }
}