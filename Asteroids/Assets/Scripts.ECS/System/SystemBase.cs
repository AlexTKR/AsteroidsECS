using Scripts.CommonExtensions;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using Scripts.ECS.World;
using Scripts.PoolsAndFactories.Pools;

namespace Scripts.ECS.System
{
    public abstract class SystemBase
    {
        protected WorldBase _world;

        public virtual bool OneFrame { get; set; }
            = false;

        public virtual void Run()
        {
        }

        public virtual void Init(WorldBase world)
        {
            _world = world;
        }

        public virtual void PreInit()
        {
        }

        public virtual void Destroy()
        {
            _world = null;
        }

        protected void Recycle<T>(EntityBase[] entities, IPool<EntityBase> pool)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                entity.RemoveComponent<T>();
                entity.GetComponent<GameObjectComponent>()?.GameObject.SetActiveOptimized(false);
                _world.RemoveEntity(entity);
                pool.Return(entity);
            }
        }
    }

    public static partial class Extensions
    {
        public static SystemBase OneFrame(this SystemBase system, bool value)
        {
            system.OneFrame = value;
            return system;
        }
    }
}