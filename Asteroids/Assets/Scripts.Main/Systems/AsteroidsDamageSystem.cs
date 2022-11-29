using ECS.Systems;
using Scripts.ECS.System;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class AsteroidsDamageSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();

            var entities = _world.GetEntity<AsteroidsDamageComponent>();

            for (int i = 0; i < entities.Length; i++)
            {
                var currentEntity = entities[i];
                var damageComponent = currentEntity.GetComponent<AsteroidsDamageComponent>(true);

                if (damageComponent.ShootType is ShootType.Laser)
                    currentEntity.AddComponent(new RecyclingBigAsteroidComponent());

                if (damageComponent.ShootType is ShootType.Bullet)
                    currentEntity.AddComponent(new SpawnSmallAsteroidComponent());
            }
        }
    }
}