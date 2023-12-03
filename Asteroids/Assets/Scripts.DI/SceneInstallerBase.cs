using Scripts.Common;
using Scripts.WindowProcessor;
using Zenject;

namespace Scripts.DI
{
    public abstract class SceneInstallerBase : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IProcessTick>().To<TickProcessor>().AsSingle().NonLazy();
            Container.Bind<IProcessWindows>().To<WindowProcessor.WindowProcessor>().AsSingle().NonLazy();
            Install();
        }

        protected virtual void Install()
        {
            
        }
    }
}
