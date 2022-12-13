using System;
using Scripts.Reactive;
using UnityEngine;

namespace Scripts.ViewViewModelBehavior
{
    public interface IMainHubBehavior
    {
        IReactiveValue<float> PlayerInstantSpeed { get; }
        IReactiveValue<int> LaserCount { get; }
        IReactiveValue<float> LaserDelay { get; }
    }

    public interface IGameOverPanelBehaviour : ISetActivePanel
    {
        IReactiveValue<int> Score { get; }
        Action OnRestartButtonPressed { get; set; }
    }

    public interface ISetActivePanel
    {
        void SetActiveStatus(bool status);
    }
}