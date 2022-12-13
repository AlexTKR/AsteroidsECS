using Scripts.ViewViewModelBehavior;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class GameOverPanel : ReactivePanelBase<IGameOverPanelBehaviour>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;
        
        public override void Init(IGameOverPanelBehaviour viewModel)
        {
            base.Init(viewModel);
            _scoreText.Subscribe(viewModel.Score, value =>
            {
                _scoreText.text = $"Score: {value}";
            },ref _onDestroy);
            
            _restartButton.onClick.AddListener(() =>
            {
                _viewModel.OnRestartButtonPressed?.Invoke();
            });
        }
    }
}
