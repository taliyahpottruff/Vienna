using UnityEngine;

public class Cover : MonoBehaviour {
    public Type type = Type.Full;

    public enum Type {
        Full = 1, Half = 0
    }
}