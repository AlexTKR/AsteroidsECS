using Scripts.Reactive;
using Scripts.UI;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.ViewModel
{
    public abstract class ViewModelBase : MonoBehaviour
    {
        
    }

    public class MainHudViewModel : ViewModelBase, IMainHubBehavior
    {
        [SerializeField] private MainHudPanel _mainHudPanel;

        public IReactiveValue<Vector2> PlayerCoordinates { get; }  = new ReactiveValue<Vector2>();
        public IReactiveValue<float> PlayerTurnAngle { get; } = new ReactiveValue<float>();
        public IReactiveValue<float> PlayerInstantSpeed { get; }  = new ReactiveValue<float>();
        public IReactiveValue<float> LaserCount { get; }  = new ReactiveValue<float>();
        public IReactiveValue<float> LaserDelay { get; } = new ReactiveValue<float>();


        private void Awake()
        {
            _mainHudPanel.Init(this);
        }
    }
}