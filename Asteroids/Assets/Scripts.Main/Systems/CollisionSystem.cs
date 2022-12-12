using Leopotam.Ecs;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Converters;

namespace Scripts.Main.Systems
{
    public class CollisionSystem : IEcsRunSystem
    {
        private EcsFilter<TriggerComponent, GameObjectComponent> _triggerFilter;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            var scoreEntity = _ecsWorld.NewEntity().Get<GameScoreComponent>();
            int score = 0;

            foreach (var i in _triggerFilter)
            {
                ref var triggerEntity = ref _triggerFilter.GetEntity(i);
                ref var triggerComponent = ref _triggerFilter.Get1(i);
                var other = triggerComponent.Collider;
                triggerEntity.Del<TriggerComponent>();

                if (triggerEntity.Has<LaserComponent>())
                    continue;

                if (triggerEntity.Has<PlayerComponent>())
                {
                    triggerEntity.Get<DamageComponent>();
                    continue;
                }

                ref var gameObjectComponent = ref _triggerFilter.Get2(i);
                gameObjectComponent.GameObject.SetActiveOptimized(false);

                if (triggerEntity.Has<BigAsteroidComponent>())
                {
                    var physicsAffectedEntityToMono =
                        other.GetComponent<PhysicsAffectedEntityToMono>();
                    ref var otherEntity = ref physicsAffectedEntityToMono.Entity;

                    if (otherEntity.Has<BulletComponent>())
                    {
                        triggerEntity.Get<DamageComponent>();
                    }
                }

                triggerEntity.Get<RecyclingComponent>();

                if (!triggerEntity.Has<ScoreEntityComponent>())
                    continue;

                ref var scoreEntityComponent = ref triggerEntity.Get<ScoreEntityComponent>();
                score += scoreEntityComponent.ScoreForEntity;
            }

            scoreEntity.Score = score;
        }
    }
}