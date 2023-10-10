using System.Collections.Generic;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using Game.Shared.Damage.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Shared.UI.Services
{
    [UsedImplicitly]
    internal class HealthService : ITickable
    {
        private readonly HealthBar _prefab;
        private readonly Transform _parent;

        private Vector2 _offset;
        private readonly Dictionary<Model, HealthBar> _instancedBars = new();

        public HealthService(HealthBar prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
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
            bar.SetChasedTarget(signal.Model.View.transform);
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

        public void UpdateHealthBar(HealthChangedSignal signal)
        {
            var model = signal.Model;
            var hp = model.Health / model.MaxHealth;
            _instancedBars[signal.Model].UpdateValue(hp);
        }
    }
}