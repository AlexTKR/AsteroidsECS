using System;
using System.Threading.Tasks;
using ECS.Systems;
using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.System;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class LaserSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();

            var shootEntity = _world.GetEntity<ShootLaserComponent>();
            var laserEntity = _world.GetEntity<LaserComponent>()?.FirstIfAny();
            if(laserEntity is null)
                return;
            
            for (int i = 0; i < shootEntity.Length; i++)
            {
                var currShootEntity = shootEntity[i];
                var shootComponent = currShootEntity.GetComponent<ShootLaserComponent>(true);
                if (shootComponent is null)
                    continue;
                
                var gameObjectComponent = laserEntity.GetComponent<GameObjectComponent>();
                if (gameObjectComponent is { })
                {
                    gameObjectComponent.GameObject.SetActiveOptimized(true);
                    DisableLaserAfterDelay(gameObjectComponent);
                }
            }
        }

        private async void DisableLaserAfterDelay(GameObjectComponent gameObjectComponent)
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));
            gameObjectComponent.GameObject.SetActiveOptimized(false);
        }
    }
}