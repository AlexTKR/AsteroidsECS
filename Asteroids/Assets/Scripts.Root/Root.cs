using Leopotam.Ecs;
using Scripts.Common;
using Scripts.Data;
using Scripts.GlobalEvents;
using Scripts.Main.Controllers;
using Scripts.Main.Converters;
using Scripts.Main.Loader;
using Scripts.Main.Pools;
using Scripts.Main.Systems;
using UnityEngine;
using Zenject;

namespace Scripts.Root
{
    public class Root : MonoBehaviour
    {
        private EcsSystems _runSystems;
        private EcsSystems _physicsRunSystems;
        private EcsWorld _world;

        private IProcessTick _processTick;
        private ILoadGameEntities _loadGameEntities;
        private IScreenBoundsProvider _screenBoundsProvider;
        private IDataProvider _dataProvider;
        private EntityPoolProvider _entityPoolProvider;

        [Inject]
        private void Construct(IProcessTick processTick, IScreenBoundsProvider screenBoundsProvider, 
            IDataProvider dataProvider, ILoadGameEntities loadGameEntities, EntityPoolProvider entityPoolProvider)
        {
            _processTick = processTick;
            _screenBoundsProvider = screenBoundsProvider;
            _dataProvider = dataProvider;
            _loadGameEntities = loadGameEntities;
            _entityPoolProvider = entityPoolProvider;
        }

        private void Start()
        {
            Initiate();
        }

        private void Initiate()
        {
            _world = new EcsWorld();
            _runSystems = new EcsSystems(_world);
            _physicsRunSystems = new EcsSystems(_world);

            _runSystems
                .Inject(_dataProvider)
                .Inject(_entityPoolProvider)
                .Inject(_screenBoundsProvider)
                .Inject(_loadGameEntities)
                .Add(new PauseSystem())
                .Add(new PlayerInitSystem())
                .Add(new ScoreInitSystem())
                .Add(new PlayerDamageSystem())
                .Add(new InputSystem())
                .Add(new BigAsteroidsSpawnSystem())
                .Add(new SmallAsteroidsSpawnSystem())
                .Add(new UfoSpawnSystem())
                .Add(new BulletSystem())
                .Add(new SpawnSystem())
                .Add(new LaserSystem())
                .Add(new EntityScreenPlacementSystem())
                .Add(new MovementWithInertiaSystem())
                .Add(new RotationSystem())
                .Add(new MovementSystem())
                .Add(new ScreenBoundariesSystem())
                .Add(new RecyclingSystem())
                .Add(new GameOverSystem())
                .Add(new RestartGameSystem())
                .Add(new UiOutputSystem())
                .Init();

            _physicsRunSystems
                .Add(new CollisionSystem())
                .Init();

            _processTick.RegisterRunSystem(_runSystems);
            _processTick.RegisterPhysicsRunSystem(_physicsRunSystems);

            ShowMainHud();
        }

        private void Update()
        {
            _processTick?.Tick();
        }

        private void FixedUpdate()
        {
            _processTick.FixedTick();
        }

        private void OnDestroy()
        {
            _world?.Destroy();
            _runSystems?.Destroy();
            _physicsRunSystems?.Destroy();
        }
        
        private static void ShowMainHud()
        {
            var activateWindowEvent = EventProcessor.Get<ActivateWindowEvent>();
            activateWindowEvent.WindowId = WindowId.HudWindow;
            activateWindowEvent.TrackWindow = false;
            activateWindowEvent.ShowOnTop = true;
            activateWindowEvent.Publish();
        }
    }
}