using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class FollowTargetConverter : MonoConverterBase
    {
        public override void Convert(ref EcsEntity entity)
        {
            if (entity.Has<FollowTargetComponent>())
            {
                return;
            }

            entity.Get<FollowTargetComponent>();
        }
    }
}
