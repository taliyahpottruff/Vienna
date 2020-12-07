using System;
using System.Collections.Generic;
using UnityEngine;

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
			if (current == null) {
				Debug.Log("Wut");
			}
			current.player = new LivingData() {
				position = player.transform.position,
				species = player.species,
				firstName = player.firstName,
				lastName = player.lastName,
				hairType = player.hairType,
				topType = player.topType,
				bottomType = player.bottomType,
				hairColorR = player.animator.hairRenderer.color.r,
				hairColorG = player.animator.hairRenderer.color.g,
				hairColorB = player.animator.hairRenderer.color.b,
				topColorR = player.animator.topRenderer.color.r,
				topColorG = player.animator.topRenderer.color.g,
				topColorB = player.animator.topRenderer.color.b,
				bottomColorR = player.animator.bottomRenderer.color.r,
				bottomColorG = player.animator.bottomRenderer.color.g,
				bottomColorB = player.animator.bottomRenderer.color.b,
				health = player.health,
				maxHealth = player.maxHealth,
				inventory = player.GetInventoryItems(),
				healthEffects = player.healthEffects
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

		public static void Clear() {
			_current = new GameData();
        }

		public static void SetPlayerData(LivingData livingData) {
			_current.player = livingData;
        }
	}
}