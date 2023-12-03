using System;
using System.Collections.Generic;

namespace Scripts.GlobalEvents
{
    public static class EventProcessor
    {
        private static Dictionary<Type, object> _container =
            new Dictionary<Type, object>();

        public static T Get<T>() 
        {
            var eventType = typeof(T);

            if (_container.TryGetValue(eventType, out var value))
                return (T)value;

            var newEvent = Activator.CreateInstance<T>();
            _container[eventType] = newEvent;

            return newEvent;
        }
    }
}