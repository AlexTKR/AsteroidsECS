using System;
using Scripts.CommonExtensions;
using Scripts.Reactive;
using Scripts.ViewViewModelBehavior;
using UnityEngine;

namespace Scripts.UI
{
    public static class Extensions
    {
        public static void Subscribe<T>(this MonoBehaviour gameObject, IReactiveValue<T> reactiveValue,
            Action<T> action)
        {
            reactiveValue.OnChanged += () => { action(reactiveValue.Value); };
        }
    }
    
    public abstract class ReactivePanelBase<T> : MonoBehaviour, ISetActivePanel
    {
        protected T _viewModel;
        protected Action _onDestroy;

        public virtual void Init(T viewModel)
        {
            _viewModel = viewModel;
        }

        protected virtual void OnDestroy()
        {
            _onDestroy?.Invoke();
            _onDestroy = null;
        }
        
        public void SetActiveStatus(bool status)
        {
            gameObject.SetActiveOptimized(status);
        }
    }
}