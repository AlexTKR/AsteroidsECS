using System;
using Scripts.CommonExtensions;
using Scripts.ECS.World;
using Scripts.Reactive;
using Scripts.UI;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.ViewModel
{
    public class GameOverViewModel : ViewModelBase, IGameOverPanelBehaviour
    {
        [SerializeField] private GameOverPanel _gameOverPanel;

        public IReactiveValue<int> Score { get; } = new ReactiveValue<int>();
        public Action OnRestartButtonPressed { get; set; }
        public Action OnGameOver { get; private set; }

        public override void Init(IGetControllers getControllers)
        {
            base.Init(getControllers);
            _gameOverPanel.Init(this);
            OnGameOver = () => { _gameOverPanel.gameObject.SetActiveOptimized(true); };
        }
    }
}