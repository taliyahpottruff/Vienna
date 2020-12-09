namespace Vienna.Combat {
    public interface ICombat {
        bool aiming { get; }

        void Attack();
    }
}