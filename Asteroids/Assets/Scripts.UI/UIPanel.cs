using System;
using Scripts.CommonExtensions;
using Scripts.Reactive;

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

    public class UIPanel : UIElement
    {
        public Action OnDestroyAction;

        protected virtual void OnDestroy()
        {
            OnDestroyAction?.Invoke();
            OnDestroyAction = null;
        }
    }
}