using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public abstract class MonoConverterBase : MonoBehaviour
    {
        public abstract EntityBase Convert(EntityBase entity);
    }

    public class EntityToMono : MonoBehaviour
    {
        private MonoConverterBase[] _converters;

        public virtual EntityBase Convert(EntityBase entity)
        {
            _converters ??= GetComponents<MonoConverterBase>();
            
            for (int i = 0; i < _converters.Length; i++)
            {
                _converters[i].Convert(entity);
            }

            return entity;
        }
    }

    public class PhysicsAffectedEntityToMono : EntityToMono
    {
        protected EntityBase _entity;
        public EntityBase Entity => _entity;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PhysicsAffectedEntityToMono>(out var otherEntity))
                otherEntity.Entity.AddComponent(new TriggerComponent() { Other = Entity });

            _entity.AddComponent(new TriggerComponent() { Other = otherEntity.Entity });
        }

        public override EntityBase Convert(EntityBase entity)
        {
            base.Convert(entity);
            _entity = entity;
            return entity;
        }
    }
}