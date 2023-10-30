using System;
using Game.Character.PowerUps;
using TMPro;
using UnityEngine;

namespace Game.UI.Dialogs.PowerUp
{
    internal class Element : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _modifierName;
        [SerializeField] private TMP_Text _modifierValue;

        private int _idx;
        private Action<int> _cb;

        public void SetInfo(Info info, int idx, Action<int> cb)
        {
            _name.text = info.Type.ToString();
            _description.text = info.Type.ToString();
            _modifierName.text = info.ModifierType.ToString();
            _modifierValue.text = info.ModifierValue.ToString();

            _idx = idx;
            _cb = cb;
        }

        public void Select() => _cb.Invoke(_idx);
    }
}