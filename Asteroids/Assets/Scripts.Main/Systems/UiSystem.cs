using System;
using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.ViewViewModelBehavior;

namespace Scripts.Main.Systems
{
    public class UiSystem : IEcsRunSystem, IEcsInitSystem
    {
        private IMainHubBehavior _mainHubBehavior;
        private IGameOverPanelBehaviour _gameOverPanelBehaviour;
        private ILoadScene _loadScene;

        private EcsFilter<PlayerComponent, MovableWithInertiaComponent> _playerFilter;
        private EcsFilter<PlayerComponent, DiedComponent> _playerDiedFilter;
        private EcsFilter<LaserComponent> _laserFilter;
        private EcsFilter<GameScoreComponent> _gameScoreFilter;

        public void Init()
        {
            _gameOverPanelBehaviour.OnRestartButtonPressed += () => { _loadScene.LoadScene(0); };
        }

        public void Run()
        {
            HandlePlayerOutput();
            HandleLaserOutput();
            HandlePlayerDiedOutPut();
        }

        private void HandlePlayerOutput()
        {
            if (_playerFilter.IsEmpty())
                return;

            ref var movableWithInertiaComponent = ref _playerFilter.Get2(0);
            _mainHubBehavior.PlayerInstantSpeed.Value = movableWithInertiaComponent.CurrentSpeed;
        }

        private void HandleLaserOutput()
        {
            if (_laserFilter.IsEmpty())
                return;

            ref var laserEntity = ref _laserFilter.GetEntity(0);
            ref var laserComponent = ref _laserFilter.Get1(0);
            _mainHubBehavior.LaserCount.Value = laserComponent.LaserCount;

            if (laserEntity.Has<LaserDelayComponent>())
            {
                ref var delayLaserComponent = ref laserEntity.Get<LaserDelayComponent>();
                _mainHubBehavior.LaserDelay.Value = (delayLaserComponent.DelayTimer - DateTime.Now.TimeOfDay).Seconds;
            }
        }

        private void HandlePlayerDiedOutPut()
        {
            if (_playerDiedFilter.IsEmpty())
                return;

            ref var playerEntity = ref _playerDiedFilter.GetEntity(0);
            playerEntity.Del<DiedComponent>();

            if (!_gameScoreFilter.IsEmpty())
            {
                ref var gameScoreComponent = ref _gameScoreFilter.Get1(0);
                _gameOverPanelBehaviour.Score.Value = gameScoreComponent.Score;
            }

            _gameOverPanelBehaviour.SetActiveStatus(true);
        }
    }
}