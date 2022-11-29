using UnityEngine;

namespace Scripts.ECS.Components
{
    public class GameObjectComponent : IComponent
    {
        public GameObject GameObject;
    }
    
    public class TransformComponent : IComponent
    {
        public Transform Transform;
    }

    public class SpriteRendererComponent : IComponent
    {
        public SpriteRenderer SpriteRenderer;
    }
    
    public class SpawnComponent : IComponent
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
    }
}
