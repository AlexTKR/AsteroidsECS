using Scripts.Main.Converters;
using UnityEngine;

namespace Scripts.Main.Entities
{
    public class PlayerMonoEntity : PhysicsAffectedEntityToMono
    {
        [SerializeField] private PhysicsAffectedEntityToMono laserMonoEntity;

        public PhysicsAffectedEntityToMono LaserMonoEntity => laserMonoEntity;
    }
}
