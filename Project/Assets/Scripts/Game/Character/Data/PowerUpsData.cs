using Game.Character.PowerUps;
using UnityEngine;

namespace Game.Character.Data
{
    [CreateAssetMenu(fileName = "PowerUpsData", menuName = "Game/Data/PowerUpsData", order = 1)]
    internal class PowerUpData : ScriptableObject
    {
        [field: SerializeField] public Info[] Info { get; private set; }
    }
}