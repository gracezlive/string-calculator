using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Dependencies
{
    public class Container
    {
        private const string castErrorMessage = "Type {0} does not implement interface {1}.";

        private Dictionary<string, Type> _registeredTypes = new Dictionary<string, Type>();
        private Dictionary<string, object> _instantiatedObjects = new Dictionary<string, object>();

        /// <summary>
        /// Registers a type of component by mapping it to an interface.
        /// </summary>
        /// <typeparam name="I">Type of interface</typeparam>
        /// <typeparam name="T">Type of component</typeparam>
        public void RegisterType<I, T>()
        {
            Type interfaceType = typeof(I);
            Type actualType = typeof(T);
            if (!actualType.GetInterfaces().Contains(interfaceType))
                throw new InvalidCastException(string.Format(castErrorMessage, actualType.GetType().FullName, interfaceType.FullName));


            if (_registeredTypes.ContainsKey(interfaceType.FullName))
            {
                _registeredTypes[interfaceType.FullName] = actualType;
            }
            else
            {
                _registeredTypes.Add(interfaceType.FullName, actualType);
            }
        }

        internal void CreateInstances()
        {
            _instantiatedObjects = new Dictionary<string, object>();

            foreach (string interfaceName in _registeredTypes.Keys)
            {
                Type type = _registeredTypes[interfaceName];
                _instantiatedObjects.Add(interfaceName, Activator.CreateInstance(type));
            }
        }

        internal object GetInstance<I>()
        {
            Type interfaceType = typeof(I);
            if (_instantiatedObjects.ContainsKey(interfaceType.FullName))
            {
                return _instantiatedObjects[interfaceType.FullName];
            }
            return null;
        }
    }
}
