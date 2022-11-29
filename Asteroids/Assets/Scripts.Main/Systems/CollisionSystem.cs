using ECS.Systems;
using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.System;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class CollisionSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();

            var triggerEntities = _world.GetEntity<TriggerComponent>();

            for (int i = 0; i < triggerEntities.Length; i++)
            {
                var currEntity = triggerEntities[i];
                if(currEntity.GetComponent<LaserComponent>() is {})
                    continue;
                
                var triggerComponent = currEntity.GetComponent<TriggerComponent>(true);

                if (currEntity.GetComponent<PlayerComponent>() is { })
                {
                    currEntity.AddComponent(new PlayerDamageComponent());
                    continue;
                }
                
                currEntity.GetComponent<GameObjectComponent>()?.GameObject.SetActiveOptimized(false);

                if (currEntity.GetComponent<BulletComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingBulletComponent());
                    continue;
                }
                
                   
                if (currEntity.GetComponent<BigAsteroidComponent>() is { })
                {
                    var bulletComponent = triggerComponent.Other.GetComponent<BulletComponent>();
                    var laserComponent = triggerComponent.Other.GetComponent<LaserComponent>();
                    var shootType = ShootType.Default;

                    if (bulletComponent is { })
                        shootType = ShootType.Bullet;
                    if (laserComponent is { })
                        shootType = ShootType.Laser;
                    
                    currEntity.AddComponent(new AsteroidsDamageComponent() { ShootType = shootType} );
                    continue;
                }

                if (currEntity.GetComponent<SmallAsteroidComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingSmallAsteroidComponent());
                    continue;
                }
                
                if (currEntity.GetComponent<UfoComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingUfoComponent());
                }
            }
        }
    }
}
