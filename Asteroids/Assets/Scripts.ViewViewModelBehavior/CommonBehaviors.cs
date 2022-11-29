using System;
using Scripts.Reactive;
using UnityEngine;

namespace Scripts.ViewViewModelBehavior
{
    public interface IMainHubBehavior 
    {
        IReactiveValue<Vector2> PlayerCoordinates { get; }
        IReactiveValue<float> PlayerTurnAngle { get; }
        IReactiveValue<float> PlayerInstantSpeed { get; }
        IReactiveValue<int> LaserCount { get; }
        IReactiveValue<float> LaserDelay { get; }
    }

    public interface IGameOverPanelBehaviour 
    {
        IReactiveValue<int> Score { get; }
        Action OnRestartButtonPressed { get; set; }
        Action OnGameOver { get; }
    }
}