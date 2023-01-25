using Scripts.Data;
using Zenject;

namespace Scripts.DI
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DataProvider>().AsSingle().NonLazy();
        }
    }
}