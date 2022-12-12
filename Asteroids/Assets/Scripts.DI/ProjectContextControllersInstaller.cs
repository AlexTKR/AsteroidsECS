using Controllers;
using Scripts.Main.Controllers;
using Zenject;

namespace Scripts.DI
{
    public class ProjectContextControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BundleController>().AsSingle().NonLazy();
        }
    }
}
