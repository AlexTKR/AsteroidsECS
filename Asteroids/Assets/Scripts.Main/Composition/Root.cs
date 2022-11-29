using Controllers;
using ECS.Systems;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Controllers;
using Scripts.Main.Systems;
using Scripts.Main.View;
using Scripts.ViewModel;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.Main.Composition
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private MainCanvas _mainMainCanvas;

        private SystemsBase runSystems;
        private SystemsBase physicsRunSysytems;
        private WorldBase _world;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _world = new World()
                //.InjectBehavior((IMainHubBehavior)_mainMainCanvas._mainHudViewModel)
                .InjectBehavior(new BundleController())
                .InjectBehavior(new CameraController(_mainCamera))
                .Initialize();

            runSystems = new RunSystems(_world)
                .Add(new PlayerSpawnSystem().OneFrame(true))
              //  .Add(new PlayerDamageSystem())
             //   .Add(new AsteroidsDamageSystem())
                .Add(new InputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
              //  .Add(new PlayerShootingSystem())
              //  .Add(new BulletSpawnSystem())
              //  .Add(new LaserSystem())
              //  .Add(new AsteroidsSpawnSystem())
               // .Add(new UfoSpawnSystem())
               // .Add(new MovableSystem())
                .Add(new ScreenBoundariesSystem())
                //.Add(new RecyclingSystem())
                //.Add(new UiSystem())
                .Initialize();

            physicsRunSysytems = new RunSystems(_world)
                .Add(new CollisionSystem())
                .Initialize();
        }

        private void Update()
        {
            runSystems?.Run();
        }

        private void FixedUpdate()
        {
            physicsRunSysytems?.Run();
        }

        private void OnDestroy()
        {
            runSystems?.Destroy();
            _world?.Destroy();
        }
    }
}