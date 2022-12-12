using Scripts.ViewModel;
using Scripts.ViewModel.Panels;
using Zenject;

namespace Scripts.DI
{
    public abstract class ViewModelInstallerBase : MonoInstaller
    {
        protected void BindViewModels(ViewModelBase[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                var viewModel = viewModels[i];
                Container.BindInterfacesTo(viewModel.GetType()).FromInstance(viewModel).AsSingle().NonLazy();
            }
        }
    }
}
