using Common.Installers;
using Game.Common.GameState;
using Game.Common.GameState.Signals;

namespace Game.Common
{
    [System.Serializable]
    internal class CommonInstaller : InstallerBase
    {
        protected override void BindServices()
        {
        }

        protected override void BindHandlers()
        {
            Dispatcher.Bind().Handler<TimeScaleHandler>().To<PauseGameSignal>();
            Dispatcher.Bind().Handler<TimeScaleHandler>().To<ResumeGameSignal>();
        }
    }
}