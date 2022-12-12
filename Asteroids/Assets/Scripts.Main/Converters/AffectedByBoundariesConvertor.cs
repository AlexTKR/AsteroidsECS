using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class AffectedByBoundariesConvertor : MonoConverterBase
    {
        [SerializeField] private Vector2 _boundsOffset;

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<AffectedByBoundariesComponent>() = new AffectedByBoundariesComponent()
            {
                BoundsOffset = _boundsOffset
            };
        }
    }
}
