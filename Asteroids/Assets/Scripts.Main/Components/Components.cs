using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Main.Components
{
    public struct SpawnComponent
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
        public bool IsActive;
    }

    public struct SpriteRendererComponent
    {
        public SpriteRenderer SpriteRenderer;
    }

    public struct TransformComponent
    {
        public Transform Transform;
    }

    public struct GameObjectComponent
    {
        public GameObject GameObject;
    }

    public struct TriggerComponent
    {
        public Collider Collider;
    }

    public struct DamageComponent
    {
    }

    public struct ScoreEntityComponent
    {
        public int ScoreForEntity;
    }

    public struct GameScoreComponent
    {
        public int Score;
    }

    public struct AccelerationComponent
    {
        public float Acceleration;
    }

    public struct RotationComponent
    {
        public float Rotation;
    }

    public struct RotationSpeedComponent
    {
        public float RotationSpeed;
    }

    public struct PlayerComponent
    {
        public Transform ShootTransform;
    }

    public struct PlayerDataComponent
    {
        public PlayerData PlayerData;
    }

    public struct AffectedByBoundariesComponent
    {
        public Vector2 BoundsOffset;
    }

    public struct MovableComponent
    {
        public Vector3 Direction;
        public float Speed;
    }

    public struct MovableWithInertiaComponent
    {
        public float InstantSpeed;
        public float SpeedDecreaseValue;
        public float CurrentSpeed;
        public Vector3 LastAccelerationDirection;
    }

    public struct BulletComponent
    {
    }

    public struct LaserComponent
    {
        public int LaserCount;
    }

    public struct BigAsteroidComponent
    {
    }

    public struct SmallAsteroidComponent
    {
    }

    public struct UfoComponent
    {
    }

    public struct FollowTargetComponent
    {
        public Transform Target;
    }

    public struct ShootBulletComponent
    {
    }

    public struct ShootLaserComponent
    {
    }

    public struct DelayComponent
    {
        public TimeSpan DelayTimer;
    }

    public struct SetDelayComponent
    {
        
    }

    public struct LaserActiveComponent
    {
        public TimeSpan ActiveTimer;
    }

    public struct RecyclingComponent
    {
        
    }
    
    public struct DiedComponent
    {
    }

    public struct EntityScreenPlacementComponent
    {
        
    }

    public struct RestartGameComponent
    {
        
    }

    public struct UfoSystemComponent
    {
        
    }
    
    public struct BigAsteroidSystemComponent
    {
        
    }
    
}