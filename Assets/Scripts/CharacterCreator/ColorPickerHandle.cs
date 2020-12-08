using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vienna.CharacterCreator {
    public class ColorPickerHandle : Selectable {
        public CreatorComponent component;
        public Slider brightnessSlider;

        bool dragging = false;
        Vector2 mousePosition = Vector2.zero;

        [SerializeField]
        RectTransform parent;
        Controls controls;

        protected override void Start() {
            controls = new Controls();
            controls.UI.PointerPosition.performed += PointerPosition_performed;
            controls.Enable();
        }

        private void Update() {
            if (dragging) {
                Rect clampRect = Utils.GetWorldRect(parent, Vector2.one);
                Vector2 clampedPoint = Utils.ClampToRect(mousePosition, clampRect);
                Vector2 normalizedPoint = new Vector2((clampedPoint.x - clampRect.x) / clampRect.width, (clampedPoint.y - clampRect.y) / clampRect.height);
                this.transform.position = clampedPoint;
                component.componentImage.color = Color.HSVToRGB(normalizedPoint.x, normalizedPoint.y, brightnessSlider.value);
            }
        }

        public void UpdateBrightness(float _v) {
            float h, s, v;
            Color.RGBToHSV(component.componentImage.color, out h, out s, out v);
            component.componentImage.color = Color.HSVToRGB(h, s, _v);
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