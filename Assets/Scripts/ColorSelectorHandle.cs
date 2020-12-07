using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vienna {
    public class ColorSelectorHandle : Selectable {
        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            Debug.Log("Move");
        }

        public override void OnPointerUp(PointerEventData eventData) {
            base.OnPointerUp(eventData);
            Debug.Log("Stop");
        }
    }
}