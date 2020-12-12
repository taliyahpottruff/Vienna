using System;

namespace Vienna.Items {
    [Serializable]
    public class Gun : IBaseItem, IWeapon {
        public string Name { get => name; set => name = value; }
        public string Sprite { get => sprite; set => sprite = value; }

        private string name, sprite;

        public float Attack(float attack) {
            throw new System.NotImplementedException();
        }

        public int GetAmount() {
            return 1;
        }

        public int Use(Living user) {
            Attack(25f);
            return 0;
        }
    }
}