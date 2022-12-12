using Controllers;
using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Pools;
using Scripts.Main.Systems;
using UnityEngine;
using Zenject;

namespace Scripts.Main.Composition
{
    public class Root : MonoBehaviour
    {
        private EcsSystems _runSystems;
        private EcsSystems _physicsRunSystems;
        private EcsWorld _world;
        private IInitiator _initiator;
        private IEntityPool<EcsEntity, BigAsteroidComponent> _bigAsteroidsEntityPool;
        private IEntityPool<EcsEntity, SmallAsteroidComponent> _smallAsteroidsEntityPool;
        private IEntityPool<EcsEntity, BulletComponent> _bulletEntityPool;
        private IEntityPool<EcsEntity, UfoComponent> _ufoEntityPool;

        #region Behaviours

        private ILoadPlayer _loadPlayer;
        private ILoadBullet _loadBullet;
        private IGetScreenBounds _getScreenBounds;

        #endregion

        [Inject]
        private void Construct(IInitiator initiator, ILoadPlayer loadPlayer,
            IGetScreenBounds getScreenBounds, ILoadBullet loadBullet)
        {
            _initiator = initiator;
            _loadPlayer = loadPlayer;
            _getScreenBounds = getScreenBounds;
            _loadBullet = loadBullet;
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _bigAsteroidsEntityPool = new SimpleEntityPool<EcsEntity, BigAsteroidComponent>();
            _smallAsteroidsEntityPool = new SimpleEntityPool<EcsEntity, SmallAsteroidComponent>();
            _bulletEntityPool = new SimpleEntityPool<EcsEntity, BulletComponent>();
            _ufoEntityPool = new SimpleEntityPool<EcsEntity, UfoComponent>();
            _initiator.PreInit();
            _initiator.Init();

            _world = new EcsWorld();
            _runSystems = new EcsSystems(_world);
            _physicsRunSystems = new EcsSystems(_world);

            _runSystems.Inject(_loadPlayer)
                .Inject(_loadBullet)
                .Inject(_bigAsteroidsEntityPool)
                .Inject(_smallAsteroidsEntityPool)
                .Inject(_bulletEntityPool)
                .Inject(_ufoEntityPool)
                .Inject(_getScreenBounds)
                .Add(new PlayerInitSystem())
                .Add(new InputSystem())
                .Add(new BigAsteroidsSpawnSystem())
                .Add(new SmallAsteroidsSpawnSystem())
                .Add(new UfoSpawnSystem())
                .Add(new BulletSystem())
                .Add(new SpawnSystem())
                .Add(new EntityScreenPlacementSystem())
                .Add(new MovementWithInertiaSystem())
                .Add(new RotationSystem())
                .Add(new MovableSystem())
                .Add(new ScreenBoundariesSystem())
                .Add(new RecyclingSystem())
                .Init();

            _physicsRunSystems
                .Add(new CollisionSystem())
                .Init();
        }

        private void Update()
        {
            _runSystems?.Run();
        }

        private void FixedUpdate()
        {
            _physicsRunSystems?.Run();
        }

        private void OnDestroy()
        {
            _world?.Destroy();
            _runSystems?.Destroy();
            _physicsRunSystems?.Destroy();
        }
    }
}