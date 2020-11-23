using System.Runtime.Serialization;

namespace Vienna.Items {
	public interface IBaseItem {
		string Name { get; set; }
		string Sprite { get; set; }

		int Use(Living user);
		int GetAmount();
	}
}