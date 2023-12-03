using Leopotam.Ecs;
using Scripts.Common;
using Scripts.GlobalEvents;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class UiOutputSystem : PausableSystem, IEcsInitSystem 
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<PlayerComponent, MovableWithInertiaComponent,
            PlayerDataComponent> _playerFilter;
        private EcsFilter<PlayerComponent, DiedComponent> _playerDiedFilter;

        public void Init()
        {
            EventProcessor.Get<RestartGameEvent>().OnPublish += restartGameEvent =>
            {
                _ecsWorld.NewEntity().Get<RestartGameComponent>() = new RestartGameComponent();
                EventProcessor.Get<RestartGameEvent>().UnPublish();
            };
        }

        protected override void Tick()
        {
            if (_playerFilter.IsEmpty())
                return;

            HandlePlayerMovementOutput();
            HandlePlayerDiedOutPut();
        }

        private void HandlePlayerMovementOutput()
        {
            ref var movableWithInertiaComponent = ref _playerFilter.Get2(0);
            ref var playerDataComponent = ref _playerFilter.Get3(0);

            playerDataComponent.PlayerData.InstantSpeed.Value = movableWithInertiaComponent.CurrentSpeed;
        }

        private void HandlePlayerDiedOutPut()
        {
            if (_playerDiedFilter.IsEmpty())
                return;
            
            ref var playerDiedEntity = ref _playerDiedFilter.GetEntity(0);
            playerDiedEntity.Del<DiedComponent>();

            ref SystemPausedComponent pausedComponent = ref _systemPausedFilter.Get1(0);
            pausedComponent.SystemPauseActive = true;
            
            ActivateWindowEvent activateWindowEvent = EventProcessor.Get<ActivateWindowEvent>();
            activateWindowEvent.WindowId = WindowId.GameOverWindow;
            activateWindowEvent.Publish();
        }
    }
}