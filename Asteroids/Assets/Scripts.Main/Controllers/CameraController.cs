using Scripts.Main.View;
using UnityEngine;

namespace Scripts.Main.Controllers
{
    public interface IGetScreenBounds
    {
        ref Vector2 ScreenBounds { get; }
    }

    public class CameraController : ControllersBase, IGetScreenBounds
    {
        private MainCamera _mainCamera;
        private Vector2 _screenBounds;
        
        public ref Vector2 ScreenBounds => ref _screenBounds;

        public CameraController(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
            _screenBounds = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                _mainCamera.Camera.transform.position.z) );
            _screenBounds = new Vector2(Mathf.Abs(_screenBounds.x), Mathf.Abs(_screenBounds.y));
        }
    }
}
