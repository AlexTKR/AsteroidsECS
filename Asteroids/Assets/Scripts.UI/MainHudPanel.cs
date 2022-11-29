using Scripts.ViewViewModelBehavior;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class MainHudPanel : ReactivePanelBase<IMainHubBehavior>
    {
        [SerializeField] private TextMeshProUGUI _playerCoordinatesText;
        [SerializeField] private TextMeshProUGUI _playerTurnAngleText;
        [SerializeField] private TextMeshProUGUI _playerInstantSpeedText;
        [SerializeField] private TextMeshProUGUI _laserCountText;
        [SerializeField] private TextMeshProUGUI _laserDelayText;

        public override void Init(IMainHubBehavior viewModel)
        {
            base.Init(viewModel);
            _playerCoordinatesText.Subscribe(viewModel.PlayerCoordinates,
                value => { _playerCoordinatesText.text = $"x:{value.x:F1} y:{value.y :F1}"; });
            _playerTurnAngleText.Subscribe(viewModel.PlayerTurnAngle,
                value => { _playerTurnAngleText.text = value.ToString("F1"); });
            _playerInstantSpeedText.Subscribe(viewModel.PlayerInstantSpeed,
                value => { _playerInstantSpeedText.text = value.ToString("F1"); });
            _laserCountText.Subscribe(viewModel.LaserCount, value => _laserCountText.text = value.ToString("F1"));
            _laserDelayText.Subscribe(viewModel.LaserDelay, value => _laserDelayText.text = value.ToString("F1"));
        }
    }
}