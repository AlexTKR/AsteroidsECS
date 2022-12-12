using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class InertiaMovementConverter :  MonoConverterBase
    {
        [SerializeField] private float _instantSpeed;
        [SerializeField] private float _speedDecreaseValue;
        
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<MovableWithInertiaComponent>() = new MovableWithInertiaComponent()
            {
                InstantSpeed = _instantSpeed,
                SpeedDecreaseValue = _speedDecreaseValue
            };
        }
    }
}
