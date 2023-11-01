using System;
using Scripts.CommonExtensions;
using UnityEngine;

namespace Scripts.UI
{
    public class UIElement : MonoBehaviour 
    {
        public Action OnRenderAction;

        public virtual void SetActive(bool status)
        {
            gameObject.SetActiveOptimized(status);
        }

        protected virtual void OnEnable()
        {
            OnRenderAction?.Invoke();
        }
    }
}
