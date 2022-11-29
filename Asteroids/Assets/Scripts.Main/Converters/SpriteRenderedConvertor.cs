using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class SpriteRenderedConvertor : MonoConverterBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override EntityBase Convert(EntityBase entity)
        {
            entity.AddComponent(new SpriteRendererComponent() { SpriteRenderer = _spriteRenderer });
            return entity;
        }
    }
}