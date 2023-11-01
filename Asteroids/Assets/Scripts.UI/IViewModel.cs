using System;

namespace Scripts.UI
{
    public interface IViewModel<T>
    {
        void SetActive(bool activeStatus);
        void Compose(T panel);
    }
}