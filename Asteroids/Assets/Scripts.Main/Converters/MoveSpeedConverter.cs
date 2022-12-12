using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class MoveSpeedConverter : MonoConverterBase
    {
        [SerializeField] private float _speed;

        public override void Convert(ref EcsEntity entity)
        {
            if (entity.Has<MovableComponent>())
            {
                ref var movableComponent  = ref entity.Get<MovableComponent>();
                movableComponent.Speed = _speed;
                return;
            }

            entity.Get<MovableComponent>() = new MovableComponent()
            {
                Speed = _speed,
                Direction = gameObject.transform.up
            };
        }
    }
}