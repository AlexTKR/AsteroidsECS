using Controllers;
using ECS.Systems;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Controllers;
using Scripts.Main.Systems;
using Scripts.Main.View;
using Scripts.ViewModel;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Scripts.Main.Systems.InputSystem;

namespace Scripts.Main.Composition
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private MainCanvas _mainMainCanvas;

        private SystemsBase runSystems;
        private SystemsBase physicsRunSysytems;
        private WorldBase _world;
        private IRun _runner;
        private IFixedRun _fixedRunner;
        private IPauseBehaviour _pauser;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _runner = new Runner();
            _fixedRunner = new FixedRunner();
            _pauser = new Pauser(new[] { (IPauseBehaviour)_runner, (IPauseBehaviour)_fixedRunner });

            _world = new World()
                .InjectBehavior(_pauser)
                .InjectBehavior(_mainMainCanvas._ViewModels)
                .InjectBehavior(new GameController())
                .InjectBehavior(new SceneController())
                .InjectBehavior(new BundleController())
                .InjectBehavior(new CameraController(_mainCamera))
                .Initialize();

            runSystems = new RunSystems(_world)
                .Add(new PlayerSpawnSystem().OneFrame(true))
                .Add(new PlayerDamageSystem())
                .Add(new AsteroidsDamageSystem())
                .Add(new InputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
                .Add(new BulletSpawnSystem())
                .Add(new LaserSystem())
                .Add(new AsteroidsSpawnSystem())
                .Add(new UfoSpawnSystem())
                .Add(new MovableSystem())
                .Add(new ScreenBoundariesSystem())
                .Add(new ScoreSystem())
                .Add(new RecyclingSystem())
                .Add(new UiSystem())
                .Add(new GameOverSystem())
                .Initialize();

            physicsRunSysytems = new RunSystems(_world)
                .Add(new CollisionSystem())
                .Initialize();

            _runner.SetAction(() => runSystems?.Run());
            _fixedRunner.SetAction(() => physicsRunSysytems?.Run());
        }

        private void Update()
        {
            _runner?.Run();
        }

        private void FixedUpdate()
        {
            _fixedRunner?.FixedRun();
        }

        private void OnDestroy()
        {
            runSystems?.Destroy();
            physicsRunSysytems?.Destroy();
            _world?.Destroy();
        }
    }
}