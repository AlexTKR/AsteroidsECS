using Scripts.ECS.Components;
using Scripts.ECS.Entity;

namespace Scripts.Main.Converters
{
    public class GameObjectConverter : MonoConverterBase
    {
        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new GameObjectComponent()
                { GameObject = gameObject });
        }
    }
}