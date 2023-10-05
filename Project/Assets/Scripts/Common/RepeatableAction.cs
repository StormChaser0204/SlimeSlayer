using System;
using UnityEngine;

namespace Common
{
    public class RepeatableAction
    {
        private readonly float _cooldown;
        private readonly Action _cb;

        private float _currentCooldown;
        private bool _isActive;

        public RepeatableAction(float cooldown, Action cb)
        {
            _cooldown = cooldown;
            _cb = cb;
            _isActive = true;
        }

        public void Update()
        {
            if (!_isActive)
                return;

            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                return;
            }

            _currentCooldown = _cooldown;
            _cb.Invoke();
        }

        public void Stop() => _isActive = false;
    }
}