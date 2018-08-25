using System;
using System.Collections.Generic;

namespace DependencyInjectionExample
{
    public class Container
    {
        private readonly IDictionary<Type, Type> _resolver;
        private readonly IDictionary<Type, object> _container;
        public Container()
        {
            _resolver = new Dictionary<Type, Type>();
            _container = new Dictionary<Type, object>();
        }

        public Container RegisterType<I, T>() 
            where T: I
        {
            if (!_resolver.ContainsKey(typeof(I)))
                _resolver.Add(typeof(I), typeof(T));

            return this;
        }

        public T Resolve<T>() 
        {
            if (!_resolver.ContainsKey(typeof(T)))
                throw new NotSupportedException($"No type {typeof(T)} has been registered.");

            if (_container.ContainsKey(typeof(T)))
                return (T) _container[typeof(T)];

            var instance = Activator.CreateInstance(_resolver[typeof(T)]);

            _container.Add(typeof(T), instance);

            return (T) instance;
        }
    }
}
