using UnityEngine;
using UnityEngine.UI;

namespace Vienna.CharacterCreator {
    public class BrightnessSlider : MonoBehaviour {
        public RawImage background;

        private void Start() {
            GenerateGradient();
        }

        private void GenerateGradient() {
            Texture2D texture = new Texture2D(256, 1);


            for (int x = 0; x < texture.width; x++) {
                for (int y = 0; y < texture.height; y++) {
                    float v = (float)x / (float)texture.width;
                    texture.SetPixel(x, y, Color.HSVToRGB(0, 0, v));
                }
            }
            texture.Apply();

            background.texture = texture;
        }

        public void Value(float value) {
            Debug.Log(value);
        }
    }
}
