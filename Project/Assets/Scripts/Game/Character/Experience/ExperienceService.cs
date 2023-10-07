using Game.Shared.Damage.Signals;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Character.Experience
{
    [UsedImplicitly]
    internal class ExperienceService
    {
        public void AddExp(TakerDiedSignal signal)
        {
            Debug.Log("Add exp");
        }
    }
}