using System;
using System.Collections.Generic;

namespace Vienna {
    [Serializable]
    public class HealthEffect {
        public int secondsRemaining;
        public Dictionary<string, object> effects;
        public string image = "Unknown";
    }
}