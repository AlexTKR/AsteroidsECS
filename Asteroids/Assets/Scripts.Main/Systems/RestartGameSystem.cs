using System;
using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Settings;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class RestartGameSystem : IEcsRunSystem
    {
        EcsFilter<SystemPausedComponent> _systemPausedFilter;
        private EcsFilter<RestartGameComponent> _restartGameFilter;
        private EcsFilter<PlayerComponent, GameObjectComponent, TransformComponent, MovableWithInertiaComponent> _playerFilter;
        private EcsFilter<LaserComponent, GameObjectComponent> _laserFilter;
        private EcsFilter<DelayComponent> _delayFilter;
        private EcsFilter<MovableComponent, GameObjectComponent>.Exclude<RecyclingComponent> _movableFilter;
        private EcsFilter<GameScoreComponent> _gameScoreFilter;

        public void Run()
        {
            if (_restartGameFilter.IsEmpty())
                return;

            ref var restartEntity = ref _restartGameFilter.GetEntity(0);
            restartEntity.Del<RestartGameComponent>();

            ResetMovableEntities();
            ResetGameScore();
            ResetPlayer();
            ResetDelays();
            ResetLaser();
            
            ref SystemPausedComponent pausedComponent = ref _systemPausedFilter.Get1(0);
            pausedComponent.SystemPauseActive = false;
        }

        private void ResetLaser()
        {
            ref var laserEntity = ref _laserFilter.GetEntity(0);
            laserEntity.Del<LaserActiveComponent>();
            ref LaserComponent laserComponent = ref _laserFilter.Get1(0);
            ref GameObjectComponent gameObjectComponent = ref _laserFilter.Get2(0);
            laserComponent.LaserCount = RuntimeSharedData.GameSettings.LaserCount;
            gameObjectComponent.GameObject.SetActiveOptimized(false);
        }

        private void ResetDelays()
        {
            foreach (var i in _delayFilter)
            {
                ref var delayEntity = ref _delayFilter.GetEntity(i);
                ref var delayComponent = ref delayEntity.Get<DelayComponent>();

                if (delayEntity.Has<LaserComponent>())
                {
                    delayComponent.DelayTimer = DateTime.Now.TimeOfDay;
                    continue;
                }

                delayEntity.Del<DelayComponent>();
                delayEntity.Get<SetDelayComponent>();
            }
        }

        private void ResetPlayer()
        {
            ref GameObjectComponent playerGameObjectComponent = ref _playerFilter.Get2(0);
            ref TransformComponent transformComponent = ref _playerFilter.Get3(0);
            ref MovableWithInertiaComponent movableWithInertiaComponent = ref _playerFilter.Get4(0);

            transformComponent.Transform.position = Vector3.zero;
            transformComponent.Transform.rotation = Quaternion.Euler(Vector3.zero);
            movableWithInertiaComponent.LastAccelerationDirection = Vector3.zero;
            movableWithInertiaComponent.CurrentSpeed = 0f;
            playerGameObjectComponent.GameObject.SetActiveOptimized(true);
        }

        private void ResetGameScore()
        {
            ref GameScoreComponent gameScoreComponent = ref _gameScoreFilter.Get1(0);
            gameScoreComponent.Score = 0;
        }

        private void ResetMovableEntities()
        {
            foreach (var i in _movableFilter)
            {
                ref var movableEntity = ref _movableFilter.GetEntity(i);
                movableEntity.Get<RecyclingComponent>();
            }
        }
    }
}