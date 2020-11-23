using System;
using System.Collections.Generic;

namespace Vienna.Data {
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

		#region Data
		public LivingData player;
		public List<StorageData> storages = new List<StorageData>();
		#endregion

		public static bool Save(Living player) {
			current.player = new LivingData() {
				position = player.transform.position,
				species = player.species,
				firstName = player.firstName,
				lastName = player.lastName,
				health = player.health,
				maxHealth = player.maxHealth,
				inventory = player.GetInventoryItems()
			};

			//Serialize storages
			current.storages.Clear();
			foreach (Storage storage in GameManager.singleton.GetStorages()) {
				Inventory inv = (Inventory)storage.Interact();
				current.storages.Add(new StorageData() {
					type = storage.name,
					position = storage.transform.position,
					items = inv.Items.ToArray()
				});
			}

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
}