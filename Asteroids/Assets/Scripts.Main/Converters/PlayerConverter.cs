using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class PlayerConverter : MonoConverterBase
    {
        [SerializeField] private Transform _shootTransform;

        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new PlayerComponent() { ShootTransform = _shootTransform });
        }
    }
}