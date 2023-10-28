using System;
using Common.Dialogs;
using Game.Character.PowerUps;
using UnityEngine;

namespace Game.UI.Dialogs
{
    internal class PowerUpDialog : DialogBase
    {
        [SerializeField] private Element _prefab;
        [SerializeField] private Transform _parent;

        public Action<Info> OnSelectionFinished;

        private Info[] _info;

        public void FillPowerUps(Info[] info)
        {
            for (var idx = 0; idx < info.Length; idx++)
            {
                var instance = Instantiate(_prefab, _parent);
                instance.SetInfo(info[idx], idx, FinishSelection);
            }

            _info = info;
        }

        public void FinishSelection(int idx)
        {
            OnSelectionFinished.Invoke(_info[idx]);
            Close();
        }
    }
}