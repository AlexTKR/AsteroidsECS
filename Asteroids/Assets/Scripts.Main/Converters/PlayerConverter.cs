using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class PlayerConverter : MonoConverterBase
    {
        [SerializeField] private Transform _shootTransform;

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<PlayerComponent>() = new PlayerComponent()
            {
                ShootTransform = _shootTransform
            };
        }
    }
}