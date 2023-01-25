using System;
using System.Collections.Generic;

namespace Scripts.GlobalEvents
{
    public static class EventProcessor
    {
        private static Dictionary<Type, EventBase> _container =
            new Dictionary<Type, EventBase>();

        public static EventBase Get<T>() where T : EventBase
        {
            var eventType = typeof(T);

            if (_container.TryGetValue(eventType, out var value))
                return value;

            var newEvent = Activator.CreateInstance<T>();
            _container[eventType] = newEvent;

            return newEvent;
        }
    }
}