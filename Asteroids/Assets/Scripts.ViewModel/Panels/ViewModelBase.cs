using Scripts.CommonBehaviours;
using UnityEngine;

namespace Scripts.ViewModel.Panels
{
    public abstract class ViewModelBase : MonoBehaviour, IPreInit 
    {
        public virtual void PreInit()
        {
            
        }
    }
}