using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class BulletConverter : MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<BulletComponent>() = new BulletComponent();
        }
    }
}
