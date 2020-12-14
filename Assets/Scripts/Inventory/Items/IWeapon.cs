namespace Vienna.Items {
    public interface IWeapon {
        string Sprite { get; set; }

        float Attack(float attack);
    }
}