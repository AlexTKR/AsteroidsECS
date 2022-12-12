using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class UfoConverter: MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<UfoComponent>() = new UfoComponent();
        }
    }
}
