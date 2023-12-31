﻿using Common.SignalHandler;

namespace Game.Character.Signals
{
    internal class LevelUpSignal : ISignal
    {
        public readonly int CurrentLevelExpAmount;
        public readonly int NextLevelExpAmount;
        public readonly int CurrentLevel;

        public LevelUpSignal(int currentLevelExpAmount, int nextLevelExpAmount, int currentLevel)
        {
            CurrentLevelExpAmount = currentLevelExpAmount;
            NextLevelExpAmount = nextLevelExpAmount;
            CurrentLevel = currentLevel;
        }
    }
}