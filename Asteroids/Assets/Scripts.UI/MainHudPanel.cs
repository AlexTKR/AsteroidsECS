using Scripts.ViewViewModelBehavior;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class MainHudPanel : ReactivePanelBase<IMainHubBehavior>
    {
        [SerializeField] private TextMeshProUGUI _playerInstantSpeedText;
        [SerializeField] private TextMeshProUGUI _laserCountText;
        [SerializeField] private TextMeshProUGUI _laserDelayText;

        public override void Init(IMainHubBehavior viewModel)
        {
            base.Init(viewModel);
            _playerInstantSpeedText.Subscribe(viewModel.PlayerInstantSpeed,
                value => { _playerInstantSpeedText.text = value.ToString("F1"); }, ref _onDestroy);
            _laserCountText.Subscribe(viewModel.LaserCount, value => _laserCountText.text = value.ToString("F0"),
                ref _onDestroy);
            _laserDelayText.Subscribe(viewModel.LaserDelay, value => _laserDelayText.text = value.ToString("F0"),
                ref _onDestroy);
        }
    }
}