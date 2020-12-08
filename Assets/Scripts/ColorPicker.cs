using UnityEngine;
using UnityEngine.UI;

namespace Vienna {
    [RequireComponent(typeof(RawImage))]
    public class ColorPicker : MonoBehaviour {
        public Slider brightnessSlider;

        RawImage image;
        float lastBrightness;

        private void Start() {
            image = GetComponent<RawImage>();

            lastBrightness = brightnessSlider.value;

            GenerateGradient();
        }

        private void Update() {
            if (brightnessSlider.value != lastBrightness) {
                lastBrightness = brightnessSlider.value;
                GenerateGradient();
            }
        }

        private void GenerateGradient() {
            Texture2D texture = new Texture2D(256, 256);

            
            for (int x = 0; x < texture.width; x++) {
                for (int y = 0; y < texture.height; y++) {
                    var h = (float)x / (float)texture.width;
                    float s = (float)y / (float)texture.height;
                    texture.SetPixel(x, y, Color.HSVToRGB(h, s, lastBrightness));
                }
            }
            texture.Apply();

            image.texture = texture;
        }
    }
}
