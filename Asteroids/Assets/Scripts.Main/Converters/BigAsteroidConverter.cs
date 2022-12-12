using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class BigAsteroidConverter : MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<BigAsteroidComponent>() = new BigAsteroidComponent();
        }
    }
}