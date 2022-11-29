using Scripts.ECS.Entity;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class UfoConverter: MonoConverterBase
    {
        public override EntityBase Convert(EntityBase entity)
        {
            return entity.
                AddComponent(new UfoComponent()).
                AddComponent(new FollowTargetComponent());
        }
    }
}
