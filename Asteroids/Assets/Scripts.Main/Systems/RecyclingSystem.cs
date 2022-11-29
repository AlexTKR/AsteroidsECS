using Scripts.ECS.System;
using Scripts.Main.Components;
using UnityEngine;

namespace ECS.Systems
{
    public class RecyclingSystem : SystemBase
    {
        public override void Run()
        {
            base.Run();

            var recyclingEntity = _world.GetEntity<RecyclingComponent>();

            for (int i = 0; i < recyclingEntity.Length; i++)
            {
                var entity = recyclingEntity[i];

                if (entity.GetComponent<BulletComponent>() is { })
                {
                    entity.RemoveComponent<RecyclingComponent>();
                    entity.AddComponent(new RecyclingBulletComponent());
                    continue;
                }
                
                if (entity.GetComponent<BigAsteroidComponent>() is { })
                {
                    entity.RemoveComponent<RecyclingComponent>();
                    entity.AddComponent(new RecyclingBigAsteroidComponent());
                    continue;
                }
                
                if (entity.GetComponent<SmallAsteroidComponent>() is { })
                {
                    entity.RemoveComponent<RecyclingComponent>();
                    entity.AddComponent(new RecyclingSmallAsteroidComponent());
                    continue;
                }
                
                if (entity.GetComponent<UfoComponent>() is { })
                {
                    entity.RemoveComponent<RecyclingComponent>();
                    entity.AddComponent(new RecyclingUfoComponent());
                }
            }
        }
    }
}
