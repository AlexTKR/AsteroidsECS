using System;
using System.Threading.Tasks;
using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Settings;

namespace Scripts.Main.Systems
{
    public class LaserSystem : IEcsRunSystem
    {
        private EcsFilter<LaserComponent, GameObjectComponent> _laserFilter;

        public void Run()
        {
            if (_laserFilter.IsEmpty())
                return;

            ref var laserEntity = ref _laserFilter.GetEntity(0);
            ref var laserComponent = ref _laserFilter.Get1(0);
            ref var gameObjectComponent = ref _laserFilter.Get2(0);

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

            if (laserEntity.Has<LaserDelayComponent>())
            {
                ref var laserDelayComponent = ref laserEntity.Get<LaserDelayComponent>();
                if (DateTime.Now.TimeOfDay >= laserDelayComponent.DelayTimer)
                {
                    laserEntity.Del<LaserDelayComponent>();
                    laserComponent.LaserCount = RuntimeSharedData.GameSettings.LaserCount;
                }

                return;
            }

            if (!laserEntity.Has<ShootLaserComponent>())
                return;

            laserEntity.Del<ShootLaserComponent>();

            if (--laserComponent.LaserCount <= 0)
                laserEntity.Get<LaserDelayComponent>() = new LaserDelayComponent()
                    { DelayTimer = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(20f) }; //TODO move TO game settings

            gameObjectComponent.GameObject.SetActiveOptimized(true);
            laserEntity.Get<LaserActiveComponent>() = new LaserActiveComponent()
            {
                ActiveTimer = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(1f) //TODO move TO game settings
            };
        }
    }
}