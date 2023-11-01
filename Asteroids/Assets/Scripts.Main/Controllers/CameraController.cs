using Scripts.Main.View;
using UnityEngine;
using Zenject;

namespace Scripts.Main.Controllers
{
    public interface IScreenBoundsProvider
    {
        ref Vector2 ScreenBounds { get; }
    }

    public class CameraController : ControllersBase, IScreenBoundsProvider
    {
        private MainCamera _mainCamera;
        private Vector2 _screenBounds;

        public ref Vector2 ScreenBounds => ref _screenBounds;

        [Inject]
        void Construct(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
            
            _screenBounds = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                _mainCamera.Camera.transform.position.z));
            _screenBounds = new Vector2(Mathf.Abs(_screenBounds.x), Mathf.Abs(_screenBounds.y));
        }
    }
}