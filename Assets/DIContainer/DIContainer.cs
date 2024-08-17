using System;
using System.Collections.Generic;

namespace DI
{
    internal class DIContainer
    {
        private readonly DIContainer _container;
        private readonly Dictionary<(string, Type), DiRegistration> _registrations = new();
        
        internal DIContainer(DIContainer parentContainer) => _container = parentContainer;
        
        internal void RegisterSingleton<T>(Func<DIContainer, T>  factory)
        {

        }
        
        internal void RegisterSingleton<T>(string tag, Func<DIContainer, T>  factory)
        {
        
        }
        
        internal void RegisterTransient<T>(Func<DIContainer, T>  factory)
        {

        }
        
        internal void RegisterTransient<T>(string tag, Func<DIContainer, T>  factory)
        {

        }
        
        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (_registrations.ContainsKey(key))
                throw new Exception($"DI: Factory witch tag -> {key.Item1} " +
                    $"and type -> {key.Item2.FullName} has allready registered!");

            _registrations[key] = new DiRegistration
            {
                Factory = c => factory,
                IsSiglton = isSingleton,
            };
        }

    }
}