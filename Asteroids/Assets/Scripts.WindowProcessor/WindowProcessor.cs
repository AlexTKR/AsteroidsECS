using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Scripts.Common;
using Scripts.GlobalEvents;
using Scripts.Loadable;
using Scripts.UI.Loader;
using Scripts.UI.Windows;
using Scripts.ViewModel;
using UnityEngine;
using Zenject;

namespace Scripts.WindowProcessor
{
    public interface IProcessWindows
    {
    }

    public class WindowProcessor : IProcessWindows
    {
        private ByTypeProvider _viewModelProvider;
        private ILoadUI _loadUI;

        private Dictionary<WindowId, WindowBase> _windowsContainer = new Dictionary<WindowId, WindowBase>();
        private List<WindowBase> _openWindows = new List<WindowBase>(); 
        private bool _windowInProcess;

        [Inject]
        void Construct(ILoadUI loadUI, ViewModelProvider viewModelProvider)
        {
            _loadUI = loadUI;
            _viewModelProvider = viewModelProvider;
            
            SubscribeToWindowEvents();
        }

        private void SubscribeToWindowEvents()
        {
            EventProcessor.Get<ActivateWindowEvent>().OnPublish += activationEvent =>
            {
                ProcessActivationEventAsync(activationEvent).Forget();
            };
            
            EventProcessor.Get<CloseWindowEvent>().OnPublish += closeEvent =>
            {
                ProcessCloseEventAsync(closeEvent).Forget();
            };
        }

        private async UniTaskVoid ProcessActivationEventAsync(ActivateWindowEvent activateWindowEvent)
        {
            await UniTask.WaitWhile(() => _windowInProcess);
            _windowInProcess = true;
            
            WindowId windowId = activateWindowEvent.WindowId;
            
            if (!_windowsContainer.TryGetValue(windowId, out WindowBase window))
                window = await CreateWindow(windowId);

            window ??= _windowsContainer[windowId];

            if (!activateWindowEvent.ShowOnTop)
                await CloseAll();

            await window.ShowAsync();

            if (activateWindowEvent.TrackWindow) 
                _openWindows.Add(window);
            
            _windowInProcess = false;

            if (activateWindowEvent.WindowId == windowId)
                activateWindowEvent.UnPublish();
        }
        
        private async UniTaskVoid ProcessCloseEventAsync(CloseWindowEvent closeWindowEvent)
        {
            await UniTask.WaitWhile(() => _windowInProcess);
            _windowInProcess = true;
            WindowId windowId = closeWindowEvent.WindowId;
            
            if (!_windowsContainer.TryGetValue(windowId, out WindowBase window))
                return;

            await window.HideAsync();
            
            _openWindows.Remove(window);
            _windowInProcess = false;

            if (closeWindowEvent.WindowId == windowId)
                closeWindowEvent.UnPublish();
        }

        private async UniTask CloseAll()
        {
            _windowInProcess = true;
            
            for (int i = _openWindows.Count - 1; i >= 0; i--)
            {
                await _openWindows[i].HideAsync();
            }
            
            _openWindows.Clear();
        }
        
        private async UniTask<WindowBase> CreateWindow(WindowId windowId)
        {
            ILoadable<WindowBase> loadable = _loadUI.LoadWindow(windowId);
            Task<WindowBase> loadTask = loadable.Load(autoRelease: false);
            await loadTask;
            WindowBase window = MonoBehaviour.Instantiate(loadTask.Result);
            window.InitiateViewModels(_viewModelProvider);
            _windowsContainer[windowId] = window;
            loadable.Release();
            return window;
        }
    }
}