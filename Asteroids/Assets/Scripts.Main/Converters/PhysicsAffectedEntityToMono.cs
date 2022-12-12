using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class PhysicsAffectedEntityToMono : EntityToMono
    {
        [SerializeField] protected Collider _collider;
        
        public EcsEntity Entity;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PhysicsAffectedEntityToMono>(out var physicsAffectedEntityToMono))
            {
                ref var otherEntity = ref physicsAffectedEntityToMono.Entity;
                otherEntity.Get<TriggerComponent>() = new TriggerComponent()
                {
                    Collider = _collider
                };
            }

            Entity.Get<TriggerComponent>() = new TriggerComponent()
            {
                Collider = other
            };
        }

        public override void Convert(ref EcsEntity entity)
        {
            base.Convert(ref entity);
            Entity = entity;
        }
    }
}