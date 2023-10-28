using Game.Enemies.Data;
using JetBrains.Annotations;
using Zenject;

namespace Game.Enemies.Spawn
{
    [UsedImplicitly]
    internal class CustomFactory : IFactory<Model>
    {
        private readonly ViewFactory _viewFactory;

        public CustomFactory(ViewFactory viewFactory) => _viewFactory = viewFactory;

        public Model Create()
        {
            var view = _viewFactory.Create();
            var model = new Model(view);
            return model;
        }
    }
}