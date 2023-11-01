using Scripts.CommonBehaviours;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class MainHudPanel : ViewModelConsumer
    {
        public TextMeshProUGUI PlayerInstantSpeedText;
        public TextMeshProUGUI LaserCountText;
        public TextMeshProUGUI LaserDelayText;

        private IViewModel<MainHudPanel> _viewModel;

        public override void Init(ByTypeProvider viewModelProvider)
        {
            _viewModel = viewModelProvider.Get<IViewModel<MainHudPanel>>();
            _viewModel.Compose(this);
        }
    }
}