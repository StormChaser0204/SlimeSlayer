using System.Collections.Generic;
using Common.Environment;
using Game.Environment.SettingsFiles;
using UnityEngine;

namespace Game.Environment
{
    internal class ParallaxScroller : MonoBehaviour
    {
        //TODO: move settings to scriptable object
        //TODO: group all settings to one file
        [SerializeField] private LayerSettings[] _settings;
        [SerializeField] private Data _environmentBuildData;
        [SerializeField] private View _prefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _parent;
        [SerializeField] private float _distBetweenLayerElements;

        private readonly Dictionary<int, LayerSettings> _layerSettings = new();

        private int _layerCount;
        private float[] _distRemainedByLayer;
        private Pool _pool;
        private SpawnedElements _spawnedElements;

        private void Start()
        {
            _spawnedElements = new SpawnedElements();
            _pool = new Pool(_prefab, _startPoint, _parent, _spawnedElements);

            for (var i = 0; i < _settings.Length; i++)
                _layerSettings.Add(i, _settings[i]);

            _layerCount = _settings.Length;
            _distRemainedByLayer = new float[_layerCount];
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
                var dist = _distRemainedByLayer[idx];

                if (dist >= 0)
                {
                    _distRemainedByLayer[idx] = dist + PassedDistanceByLayer(idx).x;
                    continue;
                }

                SpawnNewElementAtLayer(idx);
                _distRemainedByLayer[idx] = _distBetweenLayerElements / _layerSettings[idx].Density;
            }
        }

        private void SpawnNewElementAtLayer(int idx)
        {
            var element = _pool.GetItem();
            var settings = _layerSettings[idx];
            var additionalLayerColor = settings.AdditionalColor;
            var tr = element.transform;
            tr.localScale = new Vector3(settings.ScaleMultiplier, settings.ScaleMultiplier, 1);
            tr.localPosition += new Vector3(0, settings.OffsetY, 0);
            var mainSprite = _environmentBuildData.GetRandomTrunkSprite();
            var positions = _environmentBuildData.GetInfoByTexture(mainSprite.texture).Positions;
            var additionals = new AdditionalElementInfo[positions.Count];
            for (var i = 0; i < positions.Count; i++)
            {
                var position = positions[i];
                additionals[i].Position = position;
                additionals[i].Sprite = _environmentBuildData.GetRandomCrownSprite();
            }

            element.Init(mainSprite, additionals, idx, additionalLayerColor);
        }

        private Vector3 PassedDistanceByLayer(int layer) =>
            Vector3.left * _layerSettings[layer].SpeedMultiplier * Time.deltaTime;

        private void MoveActiveElements()
        {
            foreach (var element in _spawnedElements)
            {
                element.transform.position += PassedDistanceByLayer(element.LayerIdx);
                if (!IsEndPointReached(element) || !element.gameObject.activeInHierarchy)
                    continue;

                _pool.ReturnToPool(element);
            }
        }

        private bool IsEndPointReached(View view)
        {
            var dist = Mathf.Abs(view.CurrentPosition.x - _endPoint.position.x);
            return dist <= 0.1f;
        }
    }
}