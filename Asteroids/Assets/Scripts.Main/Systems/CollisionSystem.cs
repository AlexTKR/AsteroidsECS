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
            int score = 0;

            for (int i = 0; i < triggerEntities.Length; i++)
            {
                var currEntity = triggerEntities[i];
                if(currEntity.GetComponent<LaserComponent>() is {})
                    continue;
                
                var triggerComponent = currEntity.GetComponent<TriggerComponent>(true);

                if (currEntity.GetComponent<PlayerComponent>() is {})
                {
                    currEntity.AddComponent(new PlayerDamageComponent());
                    continue;
                }
                
                currEntity.GetComponent<GameObjectComponent>()?.GameObject.SetActiveOptimized(false);

                if (currEntity.GetComponent<BulletComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingBulletComponent());
                }

                if (currEntity.GetComponent<BigAsteroidComponent>() is { })
                {
                    var bulletComponent = triggerComponent.Other.GetComponent<BulletComponent>();
                    var shootType = ShootType.Default;

                    if (bulletComponent is { })
                        shootType = ShootType.Bullet;

                    currEntity.AddComponent(new AsteroidsDamageComponent() { ShootType = shootType} );
                }

                if (currEntity.GetComponent<SmallAsteroidComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingSmallAsteroidComponent());
                }
                
                if (currEntity.GetComponent<UfoComponent>() is { })
                {
                    currEntity.AddComponent(new RecyclingUfoComponent());
                }

                if (currEntity.GetComponent<ScoreEntityComponent>() is { } scoreEntityComponent &&
                    triggerComponent.Other.GetComponent<PlayerComponent>() is null)
                {
                    score += scoreEntityComponent.ScoreForEntity;
                }
            }

            var gameScoreEntities = _world.GetEntity<GameScoreComponent>();

            for (int i = 0; i < gameScoreEntities.Length; i++)
            {
                var gameScoreEntity = gameScoreEntities[i];
                gameScoreEntity.GetComponent<GameScoreComponent>().Score += score;
            }
        }
    }
}
