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

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value))
                    return;

                _value = value;
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;
    }
}
