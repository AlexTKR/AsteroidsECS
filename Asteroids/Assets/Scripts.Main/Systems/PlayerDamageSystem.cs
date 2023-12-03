using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class PlayerDamageSystem : PausableSystem
    {
        EcsFilter<PlayerComponent, DamageComponent, GameObjectComponent> _playerDamageFilter;

        protected override void Tick()
        {
            if (_playerDamageFilter.IsEmpty())
                return;

            ref var playerEntity = ref _playerDamageFilter.GetEntity(0);
            ref GameObjectComponent gameObjectComponent = ref _playerDamageFilter.Get3(0);

            if (!gameObjectComponent.GameObject.activeSelf)
                return;

            gameObjectComponent.GameObject.SetActiveOptimized(false);
            playerEntity.Del<DamageComponent>();
            playerEntity.Get<DiedComponent>();
        }
    }
}