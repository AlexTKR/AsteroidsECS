using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class MoveSpeedConverter : MonoConverterBase
    {
        [SerializeField] private float _speed;

        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new MovableComponent() { Speed = _speed });
        }
    }
}