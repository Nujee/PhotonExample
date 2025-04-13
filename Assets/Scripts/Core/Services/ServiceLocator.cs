using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Services
{
    public sealed class ServiceLocator : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service {type} is already registered");
                return;
            }
            _services[type] = service;
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            throw new Exception($"Service {type} not found");
        }

        public static void Clear() => _services.Clear();
    }
}
