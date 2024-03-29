using System;
using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.GlobalEvents;
using Scripts.Main.Components;
using Scripts.Main.Settings;

namespace Scripts.Main.Systems
{
    public class LaserSystem : PausableSystem, IEcsInitSystem
    {
        private EcsFilter<LaserComponent, GameObjectComponent> _laserFilter;
        private EcsFilter<PlayerDataComponent> _playerDataFilter;
        private EcsFilter<PlayerDataComponent> _playerFilter;

        private bool _dataIsSet;

        public void Init()
        {
            EventProcessor.Get<RestartGameEvent>().OnPublish += restartGameEvent =>
            {
                _dataIsSet = false;
            };
        }
        
        protected override void Tick()
        {
            if (_laserFilter.IsEmpty())
                return;
            
            ref var laserEntity = ref _laserFilter.GetEntity(0);
            ref LaserComponent laserComponent = ref _laserFilter.Get1(0);
            ref GameObjectComponent gameObjectComponent = ref _laserFilter.Get2(0);
            ref PlayerDataComponent playerDataComponent = ref _playerDataFilter.Get1(0);

            if (!_dataIsSet)
            {
                playerDataComponent.PlayerData.LaserCount.Value = laserComponent.LaserCount;
                _dataIsSet = true;
            }

            if (laserEntity.Has<LaserActiveComponent>())
            {
                ref var laserActiveComponent = ref laserEntity.Get<LaserActiveComponent>();
                if (DateTime.Now.TimeOfDay >= laserActiveComponent.ActiveTimer)
                {
                    laserEntity.Del<LaserActiveComponent>();
                    gameObjectComponent.GameObject.SetActiveOptimized(false);
                }

                return;
            }

            if (laserEntity.Has<DelayComponent>())
            {
                ref var laserDelayComponent = ref laserEntity.Get<DelayComponent>();
                playerDataComponent.PlayerData.LaserDelay.Value =
                    (laserDelayComponent.DelayTimer - DateTime.Now.TimeOfDay).Seconds;
                if (DateTime.Now.TimeOfDay >= laserDelayComponent.DelayTimer)
                {
                    laserEntity.Del<DelayComponent>();
                    laserComponent.LaserCount = RuntimeSharedData.GameSettings.LaserCount;
                    playerDataComponent.PlayerData.LaserCount.Value = laserComponent.LaserCount;
                }

                return;
            }

            if (!laserEntity.Has<ShootLaserComponent>())
                return;

            laserEntity.Del<ShootLaserComponent>();

            if (--laserComponent.LaserCount <= 0)
            {
                laserEntity.Get<DelayComponent>() = new DelayComponent()
                {
                    DelayTimer = DateTime.Now.TimeOfDay +
                                 TimeSpan.FromSeconds(RuntimeSharedData.GameSettings.LaserDelay)
                };
            }

            playerDataComponent.PlayerData.LaserCount.Value = laserComponent.LaserCount;

            gameObjectComponent.GameObject.SetActiveOptimized(true);
            laserEntity.Get<LaserActiveComponent>() = new LaserActiveComponent()
            {
                ActiveTimer = DateTime.Now.TimeOfDay +
                              TimeSpan.FromSeconds(RuntimeSharedData.GameSettings.LaserActiveTime)
            };
        }
    }
}