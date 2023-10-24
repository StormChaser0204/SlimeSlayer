using System.Collections.Generic;
using Game.Character.Signals;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.UI.Health
{
    [UsedImplicitly]
    internal class HealthService : ITickable
    {
        private readonly HealthBar _prefab;
        private readonly Transform _parent;
        private readonly Camera _uiCamera;

        private Vector2 _offset;
        private readonly Dictionary<Model, HealthBar> _instancedBars = new();

        public HealthService(HealthBar prefab, Transform parent, Camera uiCamera)
        {
            _prefab = prefab;
            _parent = parent;
            _uiCamera = uiCamera;
        }

        public void Tick()
        {
            foreach (var instancedBar in _instancedBars)
                instancedBar.Value.UpdatePosition();
        }

        public void InstantiateNewBar(Model model)
        {
            if (_instancedBars.ContainsKey(model))
            {
                Reset(model);
                return;
            }

            var bar = Object.Instantiate(_prefab, _parent);
            bar.Init(model.View.HealthBarTransform, _uiCamera);
            _instancedBars.Add(model, bar);
        }

        private void Reset(Model model)
        {
            var bar = _instancedBars[model];
            bar.gameObject.SetActive(true);
            bar.UpdateValue(1);
        }

        public void DisableHealthBar(Model model) =>
            _instancedBars[model].gameObject.SetActive(false);

        public void UpdateHealthBar(Model model)
        {
            var hp = model.Health / model.MaxHealth;
            _instancedBars[model].UpdateValue(hp);
        }
    }
}