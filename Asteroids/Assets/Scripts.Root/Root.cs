using Leopotam.Ecs;
using Scripts.Common;
using Scripts.Data;
using Scripts.Main.Controllers;
using Scripts.Main.Converters;
using Scripts.Main.Loader;
using Scripts.Main.Pools;
using Scripts.Main.Systems;
using Scripts.UI.Loader;
using Scripts.UI.Windows;
using Scripts.ViewModel;
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
        private IPauseSystems _pauseSystems;
        private ILoadGameEntities _loadGameEntities;
        private ILoadUI _loadUI;
        private IScreenBoundsProvider _screenBoundsProvider;
        private IDataProvider _dataProvider;
        private ViewModelProvider _viewModelProvider;
        private EntityPoolProvider _entityPoolProvider;


        [Inject]
        private void Construct(IPauseSystems pauseSystems,IProcessTick processTick, IScreenBoundsProvider screenBoundsProvider,
            ISceneLoader sceneLoader, IDataProvider dataProvider, ILoadGameEntities loadGameEntities,
            ILoadUI loadUI, ViewModelProvider viewModelProvider, EntityPoolProvider entityPoolProvider)
        {
            _processTick = processTick;
            _pauseSystems = pauseSystems;
            _screenBoundsProvider = screenBoundsProvider;
            _viewModelProvider = viewModelProvider;
            _dataProvider = dataProvider;
            _loadGameEntities = loadGameEntities;
            _loadUI = loadUI;
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
                .Inject(_viewModelProvider)
                .Inject(_entityPoolProvider)
                .Inject(_screenBoundsProvider)
                .Inject(_loadGameEntities)
                .Inject(_loadUI)
                .Inject(_pauseSystems)
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
                .Add(new UiSystem())
                .Init();

            _physicsRunSystems
                .Add(new CollisionSystem())
                .Init();

            _processTick.RegisterRunSystem(_runSystems);
            _processTick.RegisterPhysicsRunSystem(_physicsRunSystems);

            InitiateViews();
        }

        private void InitiateViews()
        {
            var mainWindowLoadable = _loadUI.LoadWindow(typeof(MainWindow)).Load(runAsync: false).Result;
            var mainWindow = Instantiate(mainWindowLoadable);
            mainWindow.InitiateViewModels(_viewModelProvider);
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
    }
}