using Common.SignalHandler.Handlers;
using Game.Character.Data;
using Game.Character.Services;
using Game.Enemies.Data;
using Game.Enemies.Signals;
using JetBrains.Annotations;
using Zenject;

namespace Game.Character.Handlers
{
    [UsedImplicitly]
    internal class ExperienceHandler : SignalHandler
    {
        [Inject] private ExperienceService _experienceService;
        [Inject] private ExperienceMap _experienceMap;

        private readonly Type _type;

        public ExperienceHandler(EnemyDiedSignal signal) : base(signal) =>
            _type = signal.Model.Type;

        public override void Handle() => _experienceService.AddExperience(_experienceMap[_type]);
    }
}