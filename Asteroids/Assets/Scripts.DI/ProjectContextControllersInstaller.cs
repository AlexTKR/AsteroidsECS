using Scripts.Main.Controllers;
using Zenject;

namespace Scripts.DI
{
    public class ProjectContextControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneLoaderController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BundleController>().AsSingle().NonLazy();
        }
    }
}
