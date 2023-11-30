using Scripts.Main.Controllers;
using Scripts.Main.Loader;
using Scripts.UI.Loader;
using Zenject;

namespace Scripts.DI
{
    public class ProjectContextControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneLoaderController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UILoader>().AsSingle().NonLazy();
        }
    }
}
