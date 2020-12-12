namespace Vienna.Items {
	public interface IStackable {
		int Stack { get; set; }
		int MaxStack { get; set; }

		int Remove(int amount);
	}
}