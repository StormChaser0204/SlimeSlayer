using System;
using System.Collections.Generic;

namespace Common.SignalDispatching.Binding
{
    public class Group
    {
        public IEnumerable<Type> Handlers => _handlers;

        private readonly List<Type> _handlers = new();

        public void Add(Type type) => _handlers.Add(type);
    }
}