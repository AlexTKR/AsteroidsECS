using System;
using Scripts.CommonExtensions;
using Scripts.Reactive;
using Scripts.UI;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.ViewModel.Panels
{
    public class GameOverViewModel : ViewModelBase, IGameOverPanelBehaviour
    {
        [SerializeField] private GameOverPanel _gameOverPanel;

        public IReactiveValue<int> Score { get; } = new ReactiveValue<int>();
        public Action OnRestartButtonPressed { get; set; }

        public override void PreInit()
        {
            base.PreInit();
            _gameOverPanel.Init(this);
        }

        public void SetActiveStatus(bool status)
        {
            _gameOverPanel.SetActiveStatus(status);
        }
    }
}