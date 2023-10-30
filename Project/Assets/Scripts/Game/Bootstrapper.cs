using Common.SignalDispatching.Dispatcher;
using Game.Character;
using Game.Common;
using Game.Enemies;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game
{
    internal class Bootstrapper : MonoInstaller
    {
        [SerializeField] private CommonInstaller _commonInstaller;
        [SerializeField] private CharacterInstaller _characterInstaller;
        [SerializeField] private EnemiesInstaller _enemiesInstaller;
        [SerializeField] private UIInstaller _uiInstaller;

        public override void InstallBindings()
        {
            var dispatcher = new SignalDispatcher(Container);
            Container.Bind<ISignalDispatcher>().FromInstance(dispatcher).AsSingle();

            _commonInstaller.Install(Container, dispatcher);
            _characterInstaller.Install(Container, dispatcher);
            _enemiesInstaller.Install(Container, dispatcher);
            _uiInstaller.Install(Container, dispatcher);
        }
    }
}