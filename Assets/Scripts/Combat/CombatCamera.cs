using UnityEngine;
using UnityEngine.InputSystem;

namespace Combat {
    public class CombatCamera : MonoBehaviour {
        [Range(0f, 10f)]
        public float speed = 4f;

        private Controls controls;
        private Vector2 inputVector;

        private void Start() {
            controls = new Controls();
            controls.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
            controls.Enable();
        }

        private void Update() {
            if (inputVector != Vector2.zero) {
                var forwardDirection = Vector3.Normalize(new Vector3(transform.forward.x, transform.position.y, transform.forward.z));
                var forward2D = new Vector2(forwardDirection.x, forwardDirection.z);
                var side2D = Vector2.Perpendicular(forward2D) * -inputVector.x;
                transform.Translate(forwardDirection * inputVector.y * speed * Time.deltaTime, Space.World);
                transform.Translate(new Vector3(side2D.x, 0, side2D.y) * speed * Time.deltaTime, Space.World);
            }
        }
    }
}