using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;
using Zenject;

namespace Scripts.Main.Pools
{
    public class EntityPoolProvider : ByTypeProvider
    {
        [Inject]
        private void Construct(IEntityPool<EcsEntity, BigAsteroidComponent> bigAsteroidPool, 
            IEntityPool<EcsEntity, SmallAsteroidComponent> smallAsteroidsPool, IEntityPool<EcsEntity, BulletComponent> bulletPool,
            IEntityPool<EcsEntity, UfoComponent> ufoPool)
        {
            Add(bigAsteroidPool);
            Add(smallAsteroidsPool);
            Add(bulletPool);
            Add(ufoPool);
        }
    }
}
