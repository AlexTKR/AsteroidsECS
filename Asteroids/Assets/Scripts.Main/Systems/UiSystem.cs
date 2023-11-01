using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.GlobalEvents;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.UI;
using Scripts.UI.Panels;
using Scripts.ViewModel;

namespace Scripts.Main.Systems
{
    public class UiSystem : IEcsRunSystem, IEcsInitSystem
    {
        private ViewModelProvider _viewModelProvider;
        private IViewModel<GameOverPanel> _gameOverViewModel;
        private ISceneLoader _sceneLoader;
        private EcsWorld _ecsWorld;

        private EcsFilter<PlayerComponent, MovableWithInertiaComponent,
            PlayerDataComponent> _playerFilter;

        private EcsFilter<PlayerComponent, DiedComponent> _playerDiedFilter;

        public void Init()
        {
            _gameOverViewModel = _viewModelProvider.Get<IViewModel<GameOverPanel>>();
            EventProcessor.Get<RestartGameEvent>().OnPublish += () =>
            {
                _ecsWorld.NewEntity().Get<RestartGameComponent>() = new RestartGameComponent();
                EventProcessor.Get<RestartGameEvent>().UnPublish();
            };
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            HandlePlayerMovementOutput();
            HandlePlayerDiedOutPut();
        }

        private void HandlePlayerMovementOutput()
        {
            if (IPauseBehaviour.IsPaused)
                return;

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

            _gameOverViewModel.SetActive(true);
        }
    }
}