using Scripts.ECS.Entity;
using Scripts.PoolsAndFactories.Factories;

namespace Scripts.ECS.Factories
{
    public class EntityFactory : IFactory<EntityBase>
    {
        public EntityBase Get()
        {
            return new Entity.Entity();
        }
    }
}