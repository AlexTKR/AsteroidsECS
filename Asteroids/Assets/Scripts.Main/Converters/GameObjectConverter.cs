using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class GameObjectConverter : MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<GameObjectComponent>() = new GameObjectComponent()
            {
                GameObject = gameObject
            };
        }
    }
}