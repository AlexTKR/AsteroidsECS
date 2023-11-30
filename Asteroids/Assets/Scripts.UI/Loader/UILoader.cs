using System;
using Scripts.Loadable;
using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.UI.Loader
{
    public interface ILoadUI : ILoadWindow<MainWindow>
    {
        
    }

    public interface ILoadWindow<T>
    {
        ILoadable<T> LoadWindow(Type type);
    }
    
    public class UILoader: ILoadUI
    {
        private const string MainWindowId = "MainCanvas";
        
        private ILoadable<MainWindow> _mainWindow;
        
        public ILoadable<MainWindow> LoadWindow(Type type)
        {
            if (type == typeof(MainWindow))
                return _mainWindow ??= new LoadReference<MainWindow, GameObject>(MainWindowId);

            return default;
        }
    }
}
