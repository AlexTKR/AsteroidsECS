using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class SpriteRenderedConvertor : MonoConverterBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<SpriteRendererComponent>() = new SpriteRendererComponent()
            {
                SpriteRenderer = _spriteRenderer
            };
        }
    }
}