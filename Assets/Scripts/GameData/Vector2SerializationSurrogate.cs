using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class Vector2SerializationSurrogate : ISerializationSurrogate {
	public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
		Vector2 v2 = (Vector2)obj;
		info.AddValue("x", v2.x);
		info.AddValue("y", v2.y);
	}

	public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
		Vector2 vector = (Vector2)obj;
		vector.x = (float)info.GetValue("x", typeof(float));
		vector.y = (float)info.GetValue("y", typeof(float));
		obj = vector;
		return obj;
	}
}