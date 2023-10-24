using System;
using Common.SignalHandler;

namespace Common.SignalDispatching.Binding
{
    public class Binder<TSignalHandlerBase>
    {
        private Group Group { get; set; } = new();
        private Type SignalType { get; set; }

        private readonly Action<Group, Type> _action;

        internal Binder(Action<Group, Type> adder) => _action = adder;

        public Binder<TSignalHandlerBase> Handler<T>()
            where T : TSignalHandlerBase
        {
            Add(typeof(T));
            return this;
        }

        private void Add(Type type) => Group.Add(type);

        public void To<T>() where T : ISignal
        {
            SignalType = typeof(T);
            Finish(SignalType);
        }

        private void Finish(Type eventType) => _action(Group, eventType);
    }
}