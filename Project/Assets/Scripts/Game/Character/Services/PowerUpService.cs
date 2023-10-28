using System;
using System.Collections.Generic;
using System.Linq;
using Game.Character.Data;
using Game.Character.PowerUps;
using Game.Character.PowerUps.Modifiers;
using JetBrains.Annotations;
using static Game.Character.PowerUps.Modifiers.Type;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class PowerUpService
    {
        private readonly Dictionary<PowerUps.Type, Info> _powerUps;
        private readonly CharacterInfo _characterInfo;
        private readonly List<PowerUps.Type> _active = new();
        private readonly HashSet<PowerUps.Type> _available = new();
        private readonly HashSet<PowerUps.Type> _blocked = new();

        public PowerUpService(CharacterInfo characterInfo, PowerUpData data)
        {
            _characterInfo = characterInfo;
            _powerUps = data.Info.ToDictionary(i => i.Type, i => i);
            CheckAvailable();
        }

        private void CheckAvailable()
        {
            foreach (var powerUp in _powerUps.Values)
            {
                var type = powerUp.Type;

                if (_active.Contains(type))
                    continue;
                if (_blocked.Contains(type))
                    continue;
                if (powerUp.Required.Except(_active).Any())
                    continue;

                _available.Add(type);
            }
        }

        public Info[] PickRandom(int amount)
        {
            var rnd = new Random();
            return _available.OrderBy(_ => rnd.Next()).Take(amount).Select(t => _powerUps[t]).ToArray();
        }

        public void ProcessPowerUp(Info info)
        {
            var modifier = GetModifier(info.ModifierType, info.ModifierValue);
            modifier.Process();
            _available.Remove(info.Type);
            _active.Add(info.Type);

            foreach (var blocked in info.Blocked)
            {
                _available.Remove(blocked);
                _blocked.Add(blocked);
            }

            CheckAvailable();
        }

        private ModifierBase GetModifier(PowerUps.Modifiers.Type type, int value)
        {
            return type switch
            {
                IncreaseHealth => new HealthModifier(_characterInfo, value),
                IncreaseDamage => new DamageModifier(_characterInfo, value),
                IncreaseHealthRegeneration => new HealthModifier(_characterInfo, value),
                IncreaseAttackRange => new HealthModifier(_characterInfo, value),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}