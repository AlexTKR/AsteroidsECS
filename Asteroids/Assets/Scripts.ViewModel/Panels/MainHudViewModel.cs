using Scripts.Reactive;
using Scripts.UI;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.ViewModel.Panels
{
    public class MainHudViewModel : ViewModelBase, IMainHubBehavior
    {
        [SerializeField] private MainHudPanel _mainHudPanel;

        public IReactiveValue<Vector2> PlayerCoordinates { get; } = new ReactiveValue<Vector2>();
        public IReactiveValue<float> PlayerTurnAngle { get; } = new ReactiveValue<float>();
        public IReactiveValue<float> PlayerInstantSpeed { get; } = new ReactiveValue<float>();
        public IReactiveValue<int> LaserCount { get; } = new ReactiveValue<int>();
        public IReactiveValue<float> LaserDelay { get; } = new ReactiveValue<float>();

        public override void PreInit()
        {
            base.PreInit();
            _mainHudPanel.Init(this);
        }
    }
}