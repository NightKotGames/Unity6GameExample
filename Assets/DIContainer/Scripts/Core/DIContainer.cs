using System;
using System.Collections.Generic;

namespace DI
{
    internal class DIContainer
    {
        private readonly DIContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIRegistration> _registrations = new();
        private readonly HashSet<(string, Type)> _resolutions = new();
        
        internal DIContainer(DIContainer parentContainer = null) => _parentContainer = parentContainer;
        
        internal void RegisterSingleton<T>(Func<DIContainer, T>  factory) => RegisterSingleton(null, factory);

        internal void RegisterSingleton<T>(string tag, Func<DIContainer, T>  factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, true);
        }
        
        internal void RegisterTransient<T>(Func<DIContainer, T>  factory) => RegisterSingleton(null, factory);

        internal void RegisterTransient<T>(string tag, Func<DIContainer, T>  factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, false);
        }

        internal void RegisterInstance<T>(T instance) => RegisterInstance(null, instance);

        internal void RegisterInstance<T>(string tag,T instance) 
        {
            var key = (tag, typeof(T));
            if (_registrations.ContainsKey(key))
                throw new Exception($"DI: Factory witch tag -> {key.Item1} " +
                    $"and type -> {key.Item2.FullName} has allready registered!");

            _registrations[key] = new DIRegistration
            {
                Instance = instance,
                IsSiglton = true
            };
        }
        
        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (_registrations.ContainsKey(key))
                throw new Exception($"DI: Factory witch tag -> {key.Item1} " +
                    $"and type -> {key.Item2.FullName} has allready registered!");

            _registrations[key] = new DIRegistration
            {
                Factory = c => factory(c),
                IsSiglton = isSingleton,
            };
        }

        internal T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutions.Contains(key))
                throw new Exception($"Cyclic Dependency for tag: {key.tag} and type {key.Item2.FullName} !");
            
            _resolutions.Add(key);

            try
            {
                if (_registrations.TryGetValue(key, out var registration))
                {
                    if (registration.IsSiglton == true)
                    {
                        if (registration.Instance == null && registration.Factory != null)
                        {
                            registration.Instance = registration.Factory(this);
                        }

                        return (T)registration.Instance;
                    }
                    return (T)registration.Factory(this);
                }

                if(_parentContainer != null)
                    return _parentContainer.Resolve<T>(tag);
            }
            finally 
            {
                _resolutions.Remove(key);
            }

            throw new Exception($"Couldn't find dependency for tag: {tag} and type: {key.Item2.FullName} !");

        }
    }
}