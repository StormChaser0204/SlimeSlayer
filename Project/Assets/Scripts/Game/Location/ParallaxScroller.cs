using System.Collections.Generic;
using Game.Location.SettingsFiles;
using UnityEngine;

namespace Game.Location
{
    public class ParallaxScroller : MonoBehaviour
    {
        [SerializeField] private LayerElements _elements;
        [SerializeField] private LayerSettings[] _settings;
        [SerializeField] private ElementView _prefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _parent;
        [SerializeField] private float _distBetweenLayerElements;

        private readonly Dictionary<int, Sprite[]> _layerElements = new();
        private readonly Dictionary<int, LayerSettings> _layerSettings = new();

        private int _layerCount;
        private System.Random _rand;
        private float[] _distLeftByLayer;
        private EnvironmentPool _pool;
        private SpawnedElements _spawnedElements;

        private void Start()
        {
            _spawnedElements = new SpawnedElements();
            _pool = new EnvironmentPool(_prefab, _startPoint, _parent, _spawnedElements);

            foreach (var layer in _elements.Layers)
                _layerElements.Add(layer.LayerIdx, layer.Sprites);

            for (var i = 0; i < _settings.Length; i++)
                _layerSettings.Add(i, _settings[i]);

            _layerCount = _layerSettings.Keys.Count;
            _rand = new System.Random();
            _distLeftByLayer = new float[_layerCount];
        }

        public void Update()
        {
            MoveActiveElements();
            CheckForNewElement();
        }

        private void CheckForNewElement()
        {
            for (var idx = 0; idx < _layerCount; idx++)
            {
                var dist = _distLeftByLayer[idx];

                if (dist >= 0)
                {
                    _distLeftByLayer[idx] = dist + PassedDistanceByLayer(idx).x;
                    continue;
                }

                SpawnNewElementAtLayer(idx);
                _distLeftByLayer[idx] = _distBetweenLayerElements / _layerSettings[idx].Density;
            }
        }

        private void SpawnNewElementAtLayer(int idx)
        {
            //move to pool
            var element = _pool.GetItem();
            var settings = _layerSettings[idx];
            var variants = _layerElements[idx];
            var sprite = variants[_rand.Next(variants.Length - 1)];
            var color = settings.AdditionalColor;
            var tr = element.transform;
            tr.localScale = new Vector3(settings.ScaleMultiplier, settings.ScaleMultiplier, 1);
            tr.position += new Vector3(0, settings.OffsetY, 0);
            element.Init(sprite, idx, color);
        }

        private Vector3 PassedDistanceByLayer(int layer) =>
            Vector3.left * _layerSettings[layer].SpeedMultiplier * Time.deltaTime;

        private void MoveActiveElements()
        {
            foreach (var element in _spawnedElements)
            {
                element.transform.position += PassedDistanceByLayer(element.LayerIdx);
                if (!IsEndPointReached(element))
                    continue;

                _pool.ReturnToPool(element);
            }
        }

        private bool IsEndPointReached(ElementView view)
        {
            var dist = Mathf.Abs(view.CurrentPosition.x - _endPoint.position.x);
            return dist <= 0.1f;
        }
    }
}