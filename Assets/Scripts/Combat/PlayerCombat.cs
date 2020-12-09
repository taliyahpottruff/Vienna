using UnityEngine;
using UnityEngine.InputSystem;

namespace Vienna.Combat {
    [RequireComponent(typeof(LivingAnimator))]
    public class PlayerCombat : MonoBehaviour, ICombat {
        public bool aiming { get => _aiming; }
        [SerializeField]
        private bool _aiming = false;

        Controls controls;
        Vector2 mousePosition = Vector2.zero;
        LivingAnimator animator;
        string aimingState = "";
        Direction direction = Direction.Down;

        private void Start() {
            animator = GetComponent<LivingAnimator>();

            controls = new Controls();
            controls.UI.PointerPosition.performed += SetMousePosition;
            controls.Player.Aim.performed += Aim_performed;
            controls.Player.Aim.canceled += Aim_canceled;
            controls.Enable();
        }

        private void Update() {
            if (_aiming) {
                Aim();

                if (animator.currentCoreState != aimingState) {
                    animator.currentCoreState = aimingState;
                    animator.SetDirection(false, direction);
                }
            }
        }

        public void Attack() {
            throw new System.NotImplementedException();
        }

        private void Aim() {
            Vector2 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 directionVector = (mouseWorldSpace - (Vector2)transform.position).normalized;
            var angle = 180 - Vector2.SignedAngle(Vector2.down, directionVector);
            direction = Direction.Up;
            if (angle >= 45 && angle <= 135) {
                direction = Direction.Right;
            } else if (angle > 135 && angle < 225) {
                direction = Direction.Down;
            } else if (angle >= 225 && angle <= 315) {
                direction = Direction.Left;
            }
            aimingState = $"Aim_{Utils.DirectionToString(direction)}";
        }

        private void SetMousePosition(InputAction.CallbackContext obj) {
            mousePosition = obj.ReadValue<Vector2>();
        }

        private void Aim_performed(InputAction.CallbackContext obj) {
            _aiming = true;
        }

        private void Aim_canceled(InputAction.CallbackContext obj) {
            _aiming = false;
            aimingState = "";
            animator.currentCoreState = "";
            animator.SetDirection(true);
        }
    }
}