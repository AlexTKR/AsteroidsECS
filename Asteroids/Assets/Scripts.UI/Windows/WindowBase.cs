using Cysharp.Threading.Tasks;
using Scripts.Common;
using UnityEngine;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private ViewModelConsumer[] _viewModelConsumers;

        public bool IsActive => gameObject.activeSelf;

        public virtual void InitiateViewModels(ByTypeProvider viewModelProvider)
        {
            for (int i = 0; i < _viewModelConsumers.Length; i++)
            {
                _viewModelConsumers[i].Init(viewModelProvider);
                _viewModelConsumers[i].Render();
            }
        }

        public virtual async UniTask ShowAsync()
        {
            gameObject.SetActive(true);
        }
        
        public virtual async UniTask HideAsync()
        {
            gameObject.SetActive(false);
        }
    }
}
