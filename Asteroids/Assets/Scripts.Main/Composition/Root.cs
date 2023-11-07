using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Data;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Converters;
using Scripts.Main.Pools;
using Scripts.Main.Systems;
using Scripts.UI.Canvas;
using Scripts.ViewModel;
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

        private ILoadGameEntities _loadGameEntities;
        private ILoadView _loadView;
        private ISceneLoader _sceneLoader;
        private IScreenBoundsProvider _screenBoundsProvider;
        private IDataProvider _dataProvider;
        private IPauseBehaviour _pauser;
        private ViewModelProvider _viewModelProvider;
        private EntityPoolProvider _entityPoolProvider;
        

        [Inject]
        private void Construct(IInitiator initiator, IScreenBoundsProvider screenBoundsProvider,
            ISceneLoader sceneLoader, IDataProvider dataProvider, ILoadGameEntities loadGameEntities, 
            ILoadView loadView, ViewModelProvider viewModelProvider, EntityPoolProvider entityPoolProvider)
        {
            _initiator = initiator;
            _screenBoundsProvider = screenBoundsProvider;
            _sceneLoader = sceneLoader;
            _viewModelProvider = viewModelProvider;
            _dataProvider = dataProvider;
            _loadGameEntities = loadGameEntities;
            _loadView = loadView;
            _entityPoolProvider = entityPoolProvider;
        }

        private void Start()
        {
            Initiate();
        }

        private void Initiate() 
        {
            _initiator.PreInit();
            _initiator.Init();

            _world = new EcsWorld();
            _runSystems = new EcsSystems(_world);
            _physicsRunSystems = new EcsSystems(_world);

            _pauser = new Pauser();

            _runSystems
                .Inject(_dataProvider)
                .Inject(_viewModelProvider)
                .Inject(_sceneLoader)
                .Inject(_entityPoolProvider)
                .Inject(_screenBoundsProvider)
                .Inject(_loadGameEntities)
                .Inject(_loadView)
                .Inject(_pauser)
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

            InitiateViews();
        }

        private void InitiateViews()
        {
            var mainCanvasLoadable = _loadView.LoadCanvas(typeof(MainCanvas)).Load(runAsync: false).Result;
            var mainCanvas = Instantiate(mainCanvasLoadable);
            mainCanvas.InitiateViewModels(_viewModelProvider);
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