using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class RotationSpeedConverter :  MonoConverterBase
    {
        [SerializeField] private float _rotationSpeed;
        
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<RotationSpeedComponent>() = new RotationSpeedComponent()
            {
                RotationSpeed = _rotationSpeed
            };
        }
    }
}
