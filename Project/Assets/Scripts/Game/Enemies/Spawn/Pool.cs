using Game.Enemies.Data;
using JetBrains.Annotations;

namespace Game.Enemies.Spawn
{
    [UsedImplicitly]
    internal class Pool : Zenject.MemoryPool<Model>
    {
    }
}