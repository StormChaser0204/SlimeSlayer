using Game.Character.Data;
using JetBrains.Annotations;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ActionService
    {
        private readonly StatsInfo _statsInfo;

        private float _invulnerableDuration;

        public ActionService(StatsInfo info) => _statsInfo = info;

        public void IncreaseDamage() => _statsInfo.UpdateDamage(2);
    }
}