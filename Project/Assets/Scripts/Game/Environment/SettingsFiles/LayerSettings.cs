using UnityEngine;

namespace Game.Environment.SettingsFiles
{
    [System.Serializable]
    public struct LayerSettings
    {
        public Color AdditionalColor;
        public float SpeedMultiplier;
        public float ScaleMultiplier;
        public float Density;
        public float OffsetY;
    }
}