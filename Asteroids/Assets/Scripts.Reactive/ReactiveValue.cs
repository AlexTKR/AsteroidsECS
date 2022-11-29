using System;

namespace Scripts.Reactive
{
    public interface IRaiser
    {
        event Action OnChanged;
    }

    public interface IReactiveValue<T> : IRaiser
    {
        T Value { get; set; }
    }

    public class ReactiveValue<T> : IReactiveValue<T>
    {
        private T _value;
        private bool _initialSet;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value) && _initialSet)
                    return;

                _value = value;
                _initialSet = true;
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;
    }
}
