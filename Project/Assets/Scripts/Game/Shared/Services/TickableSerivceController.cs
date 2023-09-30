using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared.Services
{
    internal class TickableSerivceController : MonoBehaviour
    {
        private readonly List<ITickable> _services = new();

        public void CheckAndAddService(object service)
        {
            if (service is not ITickable tickable)
                return;

            _services.Add(tickable);
        }

        public void Update() => _services.ForEach(s => s.Tick());
    }
}