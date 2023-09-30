using System;
using System.Collections.Generic;

namespace Game.Shared.Services
{
    public class ServiceLocator : ILocator, IProvider
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        private readonly Dictionary<Type, object> _services = new();

        public void Provide<T>(T serviceInstance)
        {
            var type = typeof(T);
            _services[type] = serviceInstance;
            OnNewServiceAdded.Invoke(serviceInstance);
        }

        public T Get<T>() => (T) _services[typeof(T)];

        public Action<object> OnNewServiceAdded;
    }
}