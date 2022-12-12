using Scripts.ViewModel.Canvas;
using UnityEngine;

namespace Scripts.DI
{
    public class MainSceneViewModelInstaller : ViewModelInstallerBase
    {
        [SerializeField] private CanvasViewModelBase _mainCanvas;

        public override void InstallBindings()
        {
            BindViewModels(_mainCanvas.ViewModels);
        }
    }
}
