using Scripts.ECS.Components;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Components;
using Scripts.ViewViewModelBehavior;

namespace Scripts.Main.Systems
{
    public class UiSystem : SystemBase
    {
        private IMainHubBehavior _mainHubBehavior;

        public override void Init(WorldBase world)
        {
            base.Init(world);
            _mainHubBehavior = _world.GetBehavior<IMainHubBehavior>();
        }

        public override void Run()
        {
            base.Run();

            var playerEntities = _world.GetEntity<PlayerComponent>();

            for (int i = 0; i < playerEntities.Length; i++)
            {
                var playerEntity = playerEntities[i];
                var pos = playerEntity.GetComponent<TransformComponent>()?.Transform.position;
                if (pos is { })
                    _mainHubBehavior.PlayerCoordinates.Value = pos.Value;
            }
        }
    }
}