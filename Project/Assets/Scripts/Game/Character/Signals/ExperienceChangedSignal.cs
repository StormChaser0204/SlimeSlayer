namespace Game.Character.Signals
{
    internal class ExperienceChangedSignal
    {
        public readonly int Current;

        public ExperienceChangedSignal(int current) =>
            Current = current;
    }
}