using UnityEngine;

namespace Combat {
    [RequireComponent(typeof(Camera))]
    public class CombatCamera : MonoBehaviour {
        public Camera camera;
        public Vector3 target;
    }
}