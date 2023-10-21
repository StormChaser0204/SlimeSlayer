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

        public void InstantiateNewBar(SpawnEnemySignal signal)
        {
            if (_instancedBars.ContainsKey(signal.Model))
            {
                Reset(signal.Model);
                return;
            }

            var bar = Object.Instantiate(_prefab, _parent);
            bar.Init(signal.Model.View.HealthBarTransform, _uiCamera);
            _instancedBars.Add(signal.Model, bar);
        }

        private void Reset(Model model)
        {
            var bar = _instancedBars[model];
            bar.gameObject.SetActive(true);
            bar.UpdateValue(1);
        }

        public void ReturnBar(EnemyDiedSignal signal) =>
            _instancedBars[signal.Model].gameObject.SetActive(false);

        public void UpdateHealthBar(EnemyHealthChangedSignal signal)
        {
            var model = signal.Model;
            var hp = model.Health / model.MaxHealth;
            _instancedBars[signal.Model].UpdateValue(hp);
        }
    }
}