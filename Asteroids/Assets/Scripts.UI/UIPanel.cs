using System;
using Scripts.Reactive;
using UnityEngine;

namespace Scripts.UI
{
    public static partial class Extensions
    {
        public static void Subscribe<T>(this UIPanel panel, IReactiveValue<T> reactiveValue,
            Action<T> action)
        {
            void OnChanged(T value)
            {
                if (panel.gameObject.activeSelf)
                    action?.Invoke(value);
            }

            reactiveValue.OnChanged += OnChanged;
            panel.OnDestroyAction += () => { reactiveValue.OnChanged -= OnChanged; };
            panel.OnRenderAction += () => { OnChanged(reactiveValue.Value); };
        }
    }

    public abstract class UIPanel : MonoBehaviour
    {
        public Action OnDestroyAction;
        public Action OnRenderAction;

        protected virtual void OnEnable()
        {
            Render();
        }

        protected virtual void OnDestroy()
        {
            OnDestroyAction?.Invoke();
            OnDestroyAction = null;
        }

        public virtual void Render()
        {
            OnRenderAction?.Invoke();
        }
    }
}