using Scripts.ViewModel;
using Zenject;

namespace Scripts.DI
{
    public class ViewModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo(typeof(MainHudViewModel)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(GameOverViewModel)).AsSingle().NonLazy();
            Container.Bind<ViewModelProvider>().AsSingle().NonLazy();
        }
    }
}