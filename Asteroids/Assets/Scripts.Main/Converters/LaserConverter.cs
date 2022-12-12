using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class LaserConverter : MonoConverterBase
    {
        [SerializeField] private int _laserInitialCount;

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<LaserComponent>() = new LaserComponent()
            {
                LaserCount = _laserInitialCount
            };
        }
    }
}