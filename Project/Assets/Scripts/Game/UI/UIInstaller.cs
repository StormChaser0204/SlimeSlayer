using Common.Dialogs;
using Common.Installers;
using Game.Character.Data;
using Game.Character.Signals;
using Game.Enemies.Signals;
using Game.UI.Handlers;
using Game.UI.Health;
using UnityEngine;

namespace Game.UI
{
    [System.Serializable]
    public class UIInstaller : InstallerBase
    {
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _healthBarParent;
        [SerializeField] private CharacterPanel _characterPanel;
        [SerializeField] private DialogsLauncher _dialogsLauncher;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private ExperienceData _experienceData;

        protected override void BindServices()
        {
            Container.BindInstance(_characterPanel);
            _characterPanel.Init(0, _experienceData.ExpForNewLevel, _mainCamera);

            Container.BindInterfacesTo<DialogsLauncher>().FromInstance(_dialogsLauncher).AsSingle();

            Container.BindInterfacesAndSelfTo<HealthService>().AsSingle()
                .WithArguments(_healthBarPrefab, _healthBarParent, _mainCamera);
        }

        protected override void BindHandlers()
        {
            Dispatcher.Bind().Handler<EnemyHealthBarInstanceHandler>().To<SpawnEnemySignal>();
            Dispatcher.Bind().Handler<EnemyHealthBarInstanceHandler>().To<EnemyDiedSignal>();
            Dispatcher.Bind().Handler<EnemyHealthBarValueHandler>().To<EnemyHealthChangedSignal>();

            Dispatcher.Bind().Handler<CharacterNewLevelHandler>().To<LevelUpSignal>();
            Dispatcher.Bind().Handler<CharacterLevelProgressHandler>().To<ExperienceChangedSignal>();
            Dispatcher.Bind().Handler<CharacterHealthBarValueHandler>()
                .To<CharacterHealthChangedSignal>();
        }
    }
}