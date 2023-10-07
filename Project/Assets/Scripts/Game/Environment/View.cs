using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Environment
{
    internal class View : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _main;
        [SerializeField] private SpriteRenderer[] _elements;
        [SerializeField] private SortingGroup _sortingGroup;
        
        public int LayerIdx { get; private set; }

        public Vector2 CurrentPosition => transform.position;

        public void Init(Sprite main, AdditionalElementInfo[] additionalElements, int layerIdx,
            Color additionalColor)
        {
            _main.sprite = main;
            _main.color = additionalColor;
            _sortingGroup.sortingOrder = layerIdx;
            LayerIdx = layerIdx;

            for (var i = 0; i < additionalElements.Length; i++)
            {
                var info = additionalElements[i];
                var element = _elements[i];
                element.gameObject.SetActive(true);
                element.transform.localPosition = info.Position;
                element.sprite = info.Sprite;
                element.color = additionalColor;
            }
        }

        public void Dispose()
        {
            foreach (var element in _elements)
                element.gameObject.SetActive(false);
        }
    }
}