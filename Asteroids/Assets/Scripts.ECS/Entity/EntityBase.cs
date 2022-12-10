using System;
using System.Collections.Generic;
using Scripts.ECS.Components;

namespace Scripts.ECS.Entity
{
    public abstract class EntityBase
    {
        protected Dictionary<Type, IComponent> _components =
            new Dictionary<Type, IComponent>();

        public virtual EntityBase AddComponent(IComponent component)
        {
            var type = component.GetType();
            _components[type] = component;
            return this;
        }

        public T GetComponent<T>(bool remove = false) where  T : IComponent
        {
            var type = typeof(T);
            if (!_components.TryGetValue(type, out var component))
                return default;

            if (remove)
                RemoveComponent(type);
            

            return (T)component;
        }

        private void RemoveComponent(Type type)
        {
            if(!_components.ContainsKey(type))
                return;

            _components.Remove(type);
        }
        
        public void RemoveComponent<T>()
        {
           RemoveComponent(typeof(T));
        }

        public void Destroy()
        {
            _components = null;
        }
    }

    public class Entity : EntityBase
    {
        
    }
}
