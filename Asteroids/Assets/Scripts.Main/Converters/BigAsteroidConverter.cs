using Scripts.ECS.Entity;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class BigAsteroidConverter : MonoConverterBase 
    {
        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new BigAsteroidComponent());
        }
    }
}
