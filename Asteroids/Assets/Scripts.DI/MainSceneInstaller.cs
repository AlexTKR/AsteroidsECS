using Leopotam.Ecs;
using Scripts.Main.Components;
using Scripts.Main.Composition;
using Scripts.Main.Controllers;
using Scripts.Main.Pools;
using Scripts.Main.View;
using UnityEngine;

namespace Scripts.DI
{
    public class MainSceneInstaller : SceneInstallerBase
    {
        [SerializeField] private MainCamera _mainCamera;

        protected override void Install()
        {
            Container.Bind<MainCamera>().FromInstance(_mainCamera).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
            
            Container.Bind<IEntityPool<EcsEntity, BigAsteroidComponent>>()
                .To<SimpleEntityPool<EcsEntity, BigAsteroidComponent>>().AsSingle().NonLazy();
            Container.Bind<IEntityPool<EcsEntity, SmallAsteroidComponent>>()
                .To<SimpleEntityPool<EcsEntity, SmallAsteroidComponent>>().AsSingle().NonLazy();
            Container.Bind<IEntityPool<EcsEntity, BulletComponent>>()
                .To<SimpleEntityPool<EcsEntity,  BulletComponent>>().AsSingle().NonLazy();
            Container.Bind<IEntityPool<EcsEntity, UfoComponent>>()
                .To<SimpleEntityPool<EcsEntity, UfoComponent>>().AsSingle().NonLazy();

            Container.Bind<EntityPoolProvider>().AsSingle().NonLazy();
            Container.InstantiateComponentOnNewGameObject<Root>();
        }
    }
}
