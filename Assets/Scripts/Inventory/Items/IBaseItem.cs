using System.Runtime.Serialization;

public interface IBaseItem {
	string name { get; set; }
	string sprite { get; set; }

	int Use();
	int GetAmount();
}