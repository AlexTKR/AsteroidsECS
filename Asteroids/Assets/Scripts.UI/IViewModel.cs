using System;

namespace Scripts.UI
{
    public interface IViewModel<T>
    {
        void SetActiveStatus(bool activeStatus);
        
        void Compose(T panel);
    }
}