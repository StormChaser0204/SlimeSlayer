using UnityEngine;

namespace Game.Location.SettingsFiles
{
    [CreateAssetMenu(fileName = "ItemsByLayerMap", menuName = "Game/Location/ItemsByLayerMap", order = 1)]
    public class LayerElements : ScriptableObject
    {
        [field: SerializeField] public Elements[] Layers { get; private set; }

        [System.Serializable]
        public struct Elements
        {
            public int LayerIdx;
            public Sprite[] Sprites;
        }
    }
}