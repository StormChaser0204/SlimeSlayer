using System.Collections.Generic;
using Game.Minions.Signals;
using Game.Shared.Damage.Components;
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
        private readonly Dictionary<IDamageTaker, HealthBar> _instancedBars = new();

        public HealthService(HealthBar prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void InstantiateNewBar(SpawnMinionSignal signal)
        {
            if (_instancedBars.ContainsKey(signal.Model.DamageTaker))
            {
                Reset(signal.Model.DamageTaker);
                return;
            }

            var bar = Object.Instantiate(_prefab, _parent);
            bar.SetChasedTarget(signal.Model.View.transform);
            _instancedBars.Add(signal.Model.DamageTaker, bar);
        }

        private void Reset(IDamageTaker modelDamageTaker)
        {
            var bar = _instancedBars[modelDamageTaker];
            bar.gameObject.SetActive(true);
            bar.UpdateValue(1);
        }

        public void ReturnBar(TakerDiedSignal signal) =>
            _instancedBars[signal.Taker].gameObject.SetActive(false);

        public void UpdateHealth(HealthChangedSignal signal)
        {
            var taker = signal.Taker;
            var hp = (float) taker.Health / taker.MaxHealth;
            _instancedBars[taker].UpdateValue(hp);
        }

        public void Tick()
        {
            foreach (var instancedBar in _instancedBars)
                instancedBar.Value.UpdatePosition();
        }
    }
}