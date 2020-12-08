using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vienna {
    public class ColorSelectorHandle : Selectable {
        bool dragging = false;
        Vector2 mousePosition = Vector2.zero;
        
        Controls controls;

        protected override void Start() {
            controls = new Controls();
            controls.UI.PointerPosition.performed += PointerPosition_performed;
            controls.Enable();
        }

        private void Update() {
            if (dragging) {
                this.transform.position = mousePosition;
            }
        }

        private void PointerPosition_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            mousePosition = obj.ReadValue<Vector2>();
        }

        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            dragging = true;
        }

        public override void OnPointerUp(PointerEventData eventData) {
            base.OnPointerUp(eventData);
            dragging = false;
        }
    }
}