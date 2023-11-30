using Scripts.Common;
using UnityEngine;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private ViewModelConsumer[] _viewModelConsumers;

        public virtual void InitiateViewModels(ByTypeProvider viewModelProvider)
        {
            for (int i = 0; i < _viewModelConsumers.Length; i++)
            {
                _viewModelConsumers[i].Init(viewModelProvider);
            }
        }
    }
}
