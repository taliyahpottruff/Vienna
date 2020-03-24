using System;

[Serializable]
public class GameData {
	private static GameData _current;
	public static GameData current {
		get {
			if (_current == null) {
				_current = new GameData();
			}

			return _current;
		}
	}

	public LivingData player;

	public static bool Save(Living player) {
		current.player = new LivingData() {
			position = player.transform.position,
			species = player.species,
			firstName = player.firstName,
			lastName = player.lastName,
			health = player.health,
			maxHealth = player.maxHealth
		};

		return SerializationManager.Save("Save", current);
	}

	public static bool Load() {
		GameData loaded = (GameData)SerializationManager.Load("Save");
		if (loaded != null) {
			_current = loaded;
			return true;
		}

		return false;
	}
}