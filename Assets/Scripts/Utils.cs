using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vienna {
    public static class Utils {
        public static Sprite GetSpriteFromArray(int index, Sprite[] sprites, bool getLast = false) {
            if (sprites.Length < 1) return null;
            if (index >= sprites.Length) return (getLast) ? sprites[sprites.Length - 1] : sprites[0];

            return sprites[index];
        }
    }
}
