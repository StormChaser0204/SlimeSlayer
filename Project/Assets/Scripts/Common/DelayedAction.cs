using System;
using UnityEngine;

namespace Common
{
    public class DelayedAction
    {
        private readonly Action _cb;

        private float _delay;

        public DelayedAction(float delay, Action cb)
        {
            _delay = delay;
            _cb = cb;
        }

        public void SetDelay(float delay) => _delay = delay;

        public void Update()
        {
            if (_delay > 0)
            {
                _delay -= Time.deltaTime;
                return;
            }

            _cb.Invoke();
        }
    }
}