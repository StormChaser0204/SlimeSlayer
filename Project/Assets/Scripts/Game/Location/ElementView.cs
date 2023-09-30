using UnityEngine;

namespace Game.Location
{
    public class ElementView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public int LayerIdx { get; private set; }

        public Vector2 CurrentPosition => transform.position;

        public void Init(Sprite sprite, int layerIdx, Color additionalColor)
        {
            _renderer.sprite = sprite;
            _renderer.sortingOrder = layerIdx;
            _renderer.color = additionalColor;
            LayerIdx = layerIdx;
        }
    }
}