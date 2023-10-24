using System;
using System.Collections.Generic;
using System.Threading;
using Common.SignalDispatching.Binding;
using Common.SignalHandler;
using Common.SignalHandler.Handlers;
using UnityEngine;
using Zenject;

namespace Common.SignalDispatching.Dispatcher
{
    public class SignalDispatcher : ISignalDispatcher
    {
        private readonly DiContainer _container;

        private readonly IDictionary<Type, List<Group>> _handlers = new Dictionary<Type, List<Group>>();

        public SignalDispatcher(DiContainer container) => _container = container;

        Binder<SignalHandlerBase> ISignalDispatcher.Bind() => CreateBinder();

        private Binder<SignalHandlerBase> CreateBinder() => new(AddHandlersGroup);

        private void AddHandlersGroup(Group group, Type signalType)
        {
            if (_handlers.ContainsKey(signalType))
                _handlers[signalType].Add(group);
            else
                _handlers.Add(signalType, new List<Group> { group });
        }

        void ISignalDispatcher.Raise(ISignal signal)
        {
            var type = signal.GetType();
            var groups = _handlers[type].ToArray();
            if (groups.Length == 0)
            {
                if (Debug.isDebugBuild)
                    Debug.LogWarning($"Handler is missing for the signal: {type}.");

                return;
            }

            Process(signal, groups);
        }

        private void Process(ISignal signal, IEnumerable<Group> groups)
        {
            foreach (var group in groups)
            foreach (var handler in group.Handlers)
                ProcessSignal(signal, handler);
        }

        private void ProcessSignal(ISignal signal, Type handlerType)
        {
            var handlerBase = CreateHandler(signal, handlerType);
            switch (handlerBase)
            {
                case IHandler handler:
                {
                    handler.Handle();
                    break;
                }
                case ITaskHandler taskHandler:
                {
                    RunAndForget(taskHandler, default);
                    break;
                }
            }
        }

        private SignalHandlerBase CreateHandler(ISignal signal, Type handlerType)
        {
            var handler = (SignalHandlerBase) Activator.CreateInstance(handlerType, signal);
            _container.Inject(handler);
            return handler;
        }

        private static async void RunAndForget(ITaskHandler taskHandler,
            CancellationToken cancellationToken) =>
            await taskHandler.Handle(cancellationToken);
    }
}