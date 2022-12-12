using Scripts.CommonBehaviours;
using UnityEngine;
using Zenject;

namespace Scripts.DI
{
    public abstract class SceneInstallerBase : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInitiator>().To<Initiator>().AsSingle().NonLazy();
            Install();
        }

        protected virtual void Install()
        {
            
        }
    }
}
