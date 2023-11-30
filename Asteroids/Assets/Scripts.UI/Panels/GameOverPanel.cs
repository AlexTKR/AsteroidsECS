using Scripts.Common;
using TMPro;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class GameOverPanel : ViewModelConsumer
    {
        public TextMeshProUGUI ScoreText;
        public Button RestartButton;

        private IViewModel<GameOverPanel> _viewModel;

        public override void Init(ByTypeProvider viewModelProvider)
        {
            _viewModel = viewModelProvider.Get<IViewModel<GameOverPanel>>();
            _viewModel.Compose(this);
        }
    }
}