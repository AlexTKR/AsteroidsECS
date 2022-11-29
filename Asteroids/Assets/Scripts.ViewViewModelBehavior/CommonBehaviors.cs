using Scripts.Reactive;
using UnityEngine;

namespace Scripts.ViewViewModelBehavior
{
    public interface IMainHubBehavior
    {
        IReactiveValue<Vector2> PlayerCoordinates { get; }
        IReactiveValue<float> PlayerTurnAngle { get; }
        IReactiveValue<float> PlayerInstantSpeed { get; }
        IReactiveValue<float> LaserCount { get; }
        IReactiveValue<float> LaserDelay { get; }
    }
}