using System;
using Scripts.Common;
using Scripts.Loadable;
using Scripts.UI.Windows;
using UnityEngine;

namespace Scripts.UI.Loader
{
    public interface ILoadUI : ILoadWindow<WindowBase>
    {
        
    }

    public interface ILoadWindow<T>
    {
        ILoadable<T> LoadWindow(WindowId id);
    }
    
    public class UILoader: ILoadUI
    {
        private const string HudWindowId = "HudWindow";
        private const string GameOverWindowId = "GameOverWindow";
        
        private ILoadable<WindowBase> _hudWindow;
        private ILoadable<WindowBase> _gameOverWindow;
        
        public ILoadable<WindowBase> LoadWindow(WindowId id)
        {
            return id switch
            {
                WindowId.HudWindow => _hudWindow ??= new LoadReference<WindowBase, GameObject>(HudWindowId),
                WindowId.GameOverWindow => _gameOverWindow ??= new LoadReference<WindowBase, GameObject>(GameOverWindowId),
                _ => default
            };
        }
    }
}
