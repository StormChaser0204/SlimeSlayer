using UnityEngine;

namespace Game.Character.Data
{
    [CreateAssetMenu(fileName = "ExperienceData", menuName = "Game/Data/ExperienceData", order = 1)]
    internal class ExperienceData : ScriptableObject
    {
        public int ExpForNewLevel;
        public int LevelScaler;
    }
}