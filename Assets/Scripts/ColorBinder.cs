using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vienna {
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorBinder : MonoBehaviour {
        public SpriteRenderer target;

        SpriteRenderer sr;

        private void Start() {
            sr = GetComponent<SpriteRenderer>();

            sr.color = target.color;
        }

        private void Update() {
            if (sr.color != target.color) {
                sr.color = target.color;
            }
        }
    }
}