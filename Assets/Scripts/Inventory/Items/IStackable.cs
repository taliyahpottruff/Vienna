namespace Vienna.Items {
	public interface IStackable {
		int stack { get; set; }
		int maxStack { get; set; }

		int Remove(int amount);
	}
}