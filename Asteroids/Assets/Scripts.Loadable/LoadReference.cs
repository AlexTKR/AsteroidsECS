using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Scripts.Loadable
{
    public interface ILoadable<T>
    {
        Task<T> Load(bool runAsync = true);
        void Release();
    }

    public class LoadReference<T1, T2> : ILoadable<T1>
    {
        private string _id;
        private AsyncOperationHandle _handle;

        public LoadReference(string id)
        {
            _id = id;
        }

        public async Task<T1> Load(bool runAsync = true)
        {
            if (_handle.IsValid() && _handle.IsDone)
                return CastResultToTargetType();
            
            _handle = Addressables.LoadAssetAsync<T2>(_id);

            if (runAsync)
                await _handle.Task;
            else
                _handle.WaitForCompletion();

            var result = CastResultToTargetType();

            return result;
        }

        private T1 CastResultToTargetType()
        {
            var result = typeof(T2) == typeof(GameObject)
                ? ((GameObject)_handle.Result).GetComponent<T1>()
                : (T1)_handle.Result;
            return result;
        }

        public void Release()
        {
            Addressables.Release(_handle);
        }
    }
}