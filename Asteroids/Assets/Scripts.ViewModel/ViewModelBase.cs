using Scripts.CommonExtensions;
using Scripts.UI;
using UnityEngine;

namespace Scripts.ViewModel
{
    public abstract class ViewModelBase<T> : IViewModel<T> where T : MonoBehaviour
    {
        protected T panel;
        
        public virtual void SetActiveStatus(bool activeStatus)
        {
            panel.gameObject.SetActiveOptimized(activeStatus);
        }

        public virtual void Compose(T panel)
        {
            this.panel = panel;
        }
    }
}
