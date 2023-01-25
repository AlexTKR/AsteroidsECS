using Scripts.CommonBehaviours;
using Scripts.UI;
using Scripts.UI.Panels;
using Zenject;

namespace Scripts.ViewModel
{
    public class ViewModelProvider : Provider, IViewModelProvider
    {
        [Inject]
        private void Construct(IViewModel<MainHudPanel> mainHudViewModel,
            IViewModel<GameOverPanel> gameOverViewModel)
        {
            Add(mainHudViewModel);
            Add(gameOverViewModel);
        }
    }
}