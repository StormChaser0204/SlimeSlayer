using Game.Enemies.Data;
using JetBrains.Annotations;
using Zenject;

namespace Game.Enemies.Spawn
{
    [UsedImplicitly]
    internal class CustomFactory : IFactory<Model>
    {
        private readonly InstancedEnemies _instancedEnemies;
        private readonly ViewFactory _viewFactory;

        public CustomFactory(InstancedEnemies instancedEnemies, ViewFactory viewFactory)
        {
            _instancedEnemies = instancedEnemies;
            _viewFactory = viewFactory;
        }

        public Model Create()
        {
            var view = _viewFactory.Create();
            var model = new Model(view);
            _instancedEnemies.Add(model);
            return model;
        }
    }
}