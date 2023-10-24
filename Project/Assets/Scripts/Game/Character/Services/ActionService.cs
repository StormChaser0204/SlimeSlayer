using Game.Character.Data;
using JetBrains.Annotations;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class ActionService
    {
        private readonly CharacterInfo _characterInfo;

        private float _invulnerableDuration;

        public ActionService(CharacterInfo info) => _characterInfo = info;

        public void IncreaseDamage() => _characterInfo.UpdateDamage(2);
    }
}