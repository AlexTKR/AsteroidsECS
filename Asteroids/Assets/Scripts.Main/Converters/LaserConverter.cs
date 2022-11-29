using Scripts.ECS.Entity;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class LaserConverter : MonoConverterBase
    {
        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new LaserComponent());
        }
    }
}
