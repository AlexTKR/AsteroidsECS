using Scripts.Main.Components;
using Scripts.Main.Converters;
using UnityEngine;

namespace Scripts.Main.Entities
{
    public class LaserMonoEntity : PhysicsAffectedEntityToMono
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PhysicsAffectedEntityToMono>(out var otherEntity))
                otherEntity.Entity.AddComponent(new TriggerComponent() { Other = Entity });
        }
    }
}
