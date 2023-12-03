using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Scripts.UI.Windows
{
    public class GameOverWindow :  WindowBase
    {
        [SerializeField] private Transform _holder;
        [SerializeField] private float _duration;
        
        public override async UniTask ShowAsync()
        {
            _holder.localScale = Vector3.zero;
            await base.ShowAsync(); ;
            await _holder.DOScale(Vector3.one, _duration).AsyncWaitForCompletion();
        }

        public override async UniTask HideAsync()
        {
            await _holder.DOScale(Vector3.zero, _duration).AsyncWaitForCompletion();
            await base.HideAsync();
            _holder.localScale = Vector3.one;
        }
    }
}
