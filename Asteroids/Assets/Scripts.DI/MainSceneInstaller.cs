using Scripts.Main.Composition;
using Scripts.Main.Controllers;
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
            
            Container.InstantiateComponentOnNewGameObject<Root>();
        }
    }
}
