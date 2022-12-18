using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.CommonExtensions;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class PlayerDamageSystem : IEcsRunSystem
    {
        EcsFilter<PlayerComponent, DamageComponent, GameObjectComponent> _playerDamageFilter;

        public void Run()
        {
            if (IPauseBehaviour.IsPaused)
                return;
            
            if (_playerDamageFilter.IsEmpty())
                return;

            ref var playerEntity = ref _playerDamageFilter.GetEntity(0);
            ref var gameObjectComponent = ref _playerDamageFilter.Get3(0);

            if (!gameObjectComponent.GameObject.activeSelf)
                return;

            gameObjectComponent.GameObject.SetActiveOptimized(false);
            playerEntity.Del<DamageComponent>();
            playerEntity.Get<DiedComponent>();
        }
    }
}