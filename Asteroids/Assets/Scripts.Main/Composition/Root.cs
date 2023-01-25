using Controllers;
using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Data;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Converters;
using Scripts.Main.Pools;
using Scripts.Main.Systems;
using Scripts.UI.Canvas;
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

        #region InjectBehaviours

        private ILoadCanvas<MainCanvas> _loadMainCanvas;
        private ILoadPlayer _loadPlayer;
        private ILoadBullet _loadBullet;
        private ILoadScene _loadScene;
        private IGetScreenBounds _getScreenBounds;
        private IViewModelProvider _viewModelProvider;
        private IDataProvider _dataProvider;
        private IPauseBehaviour _pauser;

        #endregion

        [Inject]
        private void Construct(IInitiator initiator, ILoadPlayer loadPlayer,
            IGetScreenBounds getScreenBounds, ILoadBullet loadBullet,
            ILoadScene loadScene, ILoadCanvas<MainCanvas> loadMainCanvas,
            IViewModelProvider viewModelProvider, IDataProvider dataProvider)
        {
            _initiator = initiator;
            _loadPlayer = loadPlayer;
            _getScreenBounds = getScreenBounds;
            _loadBullet = loadBullet;
            _loadScene = loadScene;
            _loadMainCanvas = loadMainCanvas;
            _viewModelProvider = viewModelProvider;
            _dataProvider = dataProvider;
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

            _pauser = new Pauser();

            _runSystems.Inject(_dataProvider)
                .Inject(_viewModelProvider)
                .Inject(_loadPlayer)
                .Inject(_loadBullet)
                .Inject(_loadScene)
                .Inject(_bigAsteroidsEntityPool)
                .Inject(_smallAsteroidsEntityPool)
                .Inject(_bulletEntityPool)
                .Inject(_ufoEntityPool)
                .Inject(_getScreenBounds)
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

            InitUI();
        }

        private void InitUI()
        {
            var mainCanvasLoadable = _loadMainCanvas.LoadCanvas().Load(runAsync: false).Result;
            var mainCanvas = Instantiate(mainCanvasLoadable);

            for (int i = 0; i < mainCanvas.ViewModelConsumers.Length; i++)
            {
                mainCanvas.ViewModelConsumers[i].Init(_viewModelProvider);
            }
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