using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class SmallAsteroidConverter : MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<SmallAsteroidComponent>() = new SmallAsteroidComponent();
        }
    }
}
