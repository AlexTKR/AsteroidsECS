using Scripts.ECS.Entity;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class ColliderConverter : MonoConverterBase
    {
        [SerializeField] private Collider _collider;
        
        public override EntityBase Convert(EntityBase entity)
        {
            return entity;
        }
    }
}