using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Converters;
using UnityEngine;

namespace Scripts.Main.Entities
{
    public class LaserMonoEntity : PhysicsAffectedEntityToMono
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PhysicsAffectedEntityToMono>(out var physicsAffectedEntityToMono))
            {
                ref var otherEntity = ref physicsAffectedEntityToMono.Entity;
                otherEntity.Get<TriggerComponent>() = new TriggerComponent()
                {
                    Collider = _collider
                };
            }
        }
    }
}