using Scripts.Common;
using Zenject;

namespace Scripts.DI
{
    public abstract class SceneInstallerBase : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IProcessTick>().To<TickProcessor>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PauseProcessor>().AsSingle().NonLazy();
            Install();
        }

        protected virtual void Install()
        {
            
        }
    }
}
