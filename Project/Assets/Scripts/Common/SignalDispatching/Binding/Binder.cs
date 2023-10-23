using System;
using UnityEngine;

namespace Common.SignalDispatching.Binding
{
    public class Binder<SignalHandlerBase> : MonoBehaviour
    {
        private Group Group { get; set; } = new();
        private Type SignalType { get; set; }

        private readonly Action<Group, Type> _action;

        internal Binder(Action<Group, Type> adder) => _action = adder;

        public Binder<SignalHandlerBase> Handler<T>()
            where T : SignalHandlerBase
        {
            Add(typeof(T));
            return this;
        }

        private void Add(Type type) => Group.Add(type);

        public void To<T>()
        {
            SignalType = typeof(T);
            Finish(SignalType);
        }

        private void Finish(Type eventType) => _action(Group, eventType);
    }
}