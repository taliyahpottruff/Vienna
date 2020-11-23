using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vienna.Data {
	public class SerializationManager {
		public static bool Save(string saveName, object saveData) {
			BinaryFormatter bf = GetBinaryFormatter();

			string savesFolder = Application.persistentDataPath + "/Saves";
			if (!Directory.Exists(savesFolder)) {
				Directory.CreateDirectory(savesFolder);
			}

			string path = $"{savesFolder}/{saveName}.save";

			FileStream file = File.Create(path);
			bf.Serialize(file, saveData);
			file.Close();
			return true;
		}

		public static object Load(string saveName) {
			string path = Application.persistentDataPath + "/Saves/" + saveName + ".save";
			if (!File.Exists(path)) {
				return null;
			}

			BinaryFormatter bf = GetBinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);
			try {
				object save = bf.Deserialize(file);
				file.Close();
				return save;
			} catch {
				Debug.LogErrorFormat("Failed to load file at {0}", path);
				file.Close();
				return null;
			}
		}

		public static BinaryFormatter GetBinaryFormatter() {
			BinaryFormatter bf = new BinaryFormatter();
			SurrogateSelector selector = new SurrogateSelector();

			Vector2SerializationSurrogate vector2SerializationSurrogate = new Vector2SerializationSurrogate();

			selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SerializationSurrogate);

			bf.SurrogateSelector = selector;

			return bf;
		}
	}
}