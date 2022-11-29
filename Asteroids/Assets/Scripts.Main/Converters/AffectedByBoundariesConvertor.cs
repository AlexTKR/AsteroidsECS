using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class AffectedByBoundariesConvertor : MonoConverterBase
    {
        [SerializeField] private Vector2 _boundsOffset;
        
        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new AffectedByBoundariesComponent() { BoundsOffset = _boundsOffset});
        }
    }
}
