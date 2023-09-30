using Game.Shared.Services;

namespace Game.Character.Services
{
    internal class MovementService : ITickable
    {
        private readonly float _speed;

        public MovementService(float speed) => _speed = speed;

        public void Tick()
        {
        }
    }
}