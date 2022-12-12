using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class TransformConverter : MonoConverterBase
    {

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<TransformComponent>() = new TransformComponent()
            {
                Transform = gameObject.transform
            };
        }
    }
}