using Scripts.ECS.System;
using Scripts.Main.Components;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerShootingSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();
            var shootEntity = _world.GetEntity<ShootComponent>();

            for(int i = 0; i < shootEntity.Length; i++)
            {
                var currentShootEntity = shootEntity[i];
                var shootComponent = currentShootEntity.GetComponent<ShootComponent>(true);
                
                currentShootEntity.AddComponent(shootComponent.ShootType is ShootType.Bullet
                    ? new ShootBulletComponent()
                    : new ShootLaserComponent());
            }
        }
    }
}