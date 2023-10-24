using Common.SignalHandler;

namespace Game.Character.Signals
{
    internal class ExperienceChangedSignal : ISignal
    {
        public readonly int CurrentExpAmount;

        public ExperienceChangedSignal(int currentExpAmount) =>
            CurrentExpAmount = currentExpAmount;
    }
}