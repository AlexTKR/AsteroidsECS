using System;
using Scripts.ECS.Components;
using Scripts.ECS.Entity;
using UnityEngine;

namespace Scripts.Main.Components
{
    public enum ShootType
    {
        Default,
        Laser,
        Bullet
    }

    public class DestroyComponent : IComponent
    {
    }

    public class DamageComponent : IComponent
    {
    }

    public class ScoreEntityComponent : IComponent
    {
        public int ScoreForEntity;
    }

    public class GameScoreComponent : IComponent
    {
        public int Score;
    }

    public class PlayerMovementInputComponent : IComponent
    {
        public float Acceleration;
    }

    public class PlayerRotationInputComponent : IComponent
    {
        public float Rotation;
    }

    public class PlayerComponent : IComponent
    {
        public Transform ShootTransform;
        public float Acceleration;
        public Vector3 LastAccelerationDirection;
    }

    public class AffectedByBoundariesComponent : IComponent
    {
        public Vector2 BoundsOffset;
    }

    public class MovableComponent : IComponent
    {
        public Vector3 Direction;
        public float Speed;
    }

    public class BulletComponent : IComponent
    {
        
    }

    public class LaserComponent : IComponent
    {
        public int LaserCount;
    }

    public class BigAsteroidComponent : IComponent
    {
        
    }

    public class SmallAsteroidComponent : IComponent
    {
        
    }

    public class SpawnSmallAsteroidComponent : IComponent
    {
        
    }

    public class UfoComponent : IComponent
    {
       
    }

    public class FollowTargetComponent : IComponent
    {
        public Transform Target;
    }

    public class ShootBulletComponent : IComponent
    {
        
    }

    public class ShootLaserComponent : IComponent
    {
        
    }

    public class DelayLaserComponent : IComponent
    {
        public TimeSpan DelayTimer;
    }

    public class ActiveLaserComponent : IComponent
    {
        public TimeSpan ActiveTimer;
    }

    public class RecyclingComponent : IComponent
    {
        
    }

    public class RecyclingBulletComponent : IComponent
    {
    }
    
    public class RecyclingBigAsteroidComponent : IComponent
    {
    }
    
    public class RecyclingSmallAsteroidComponent : IComponent
    {
    }
    
    public class RecyclingUfoComponent : IComponent
    {
        
    }

    public class TriggerComponent : IComponent
    {
        public EntityBase Other;
    }

    public class PlayerDamageComponent : IComponent
    {
        
    }

    public class PLayerDiedComponent : IComponent
    {
    }

    public class AsteroidsDamageComponent : IComponent
    {
        public ShootType ShootType;
    }
}