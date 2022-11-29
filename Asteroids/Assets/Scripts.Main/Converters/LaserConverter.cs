using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class LaserConverter : MonoConverterBase
    {
        [SerializeField] private int _laserInitialCount;

        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new LaserComponent() { LaserCount = _laserInitialCount });
        }
    }
}