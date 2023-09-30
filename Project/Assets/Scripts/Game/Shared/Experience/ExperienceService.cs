using Game.Minions.Data;
using Game.Shared.Services;

namespace Game.Shared.Experience
{
    internal class ExperienceService
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static SpawnedUnits SpawnedUnits => Locator.Get<SpawnedUnits>();
    }
}