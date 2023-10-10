using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Services
{
    [UsedImplicitly]
    internal class CharacterMovementService : ITickable
    {
        private readonly float _speed;

        public CharacterMovementService(float speed) => _speed = speed;

        public void Tick()
        {
        }
    }
}