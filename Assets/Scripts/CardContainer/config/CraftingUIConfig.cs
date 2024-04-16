using System;
using UnityEngine;

namespace config {
    [Serializable]
    public class CraftingUIConfig {
        [SerializeField]
        public RectTransform craftArea1;
        public RectTransform craftArea2;
        public RectTransform resultArea;
    }
}
