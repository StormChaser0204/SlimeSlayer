using System;
using UnityEngine;

namespace Common
{
    public class RepeatableAction
    {
        private readonly float _cooldown;
        private readonly Action _cb;

        private float _currentCooldown;

        public RepeatableAction(float cooldown, Action cb)
        {
            _cooldown = cooldown;
            _cb = cb;
        }

        public void Update()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                return;
            }

            _currentCooldown = _cooldown;
            _cb.Invoke();
        }
    }
}