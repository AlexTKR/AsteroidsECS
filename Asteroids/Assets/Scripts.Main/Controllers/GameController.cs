using Controllers;
using Scripts.Main.Settings;
using Scripts.ViewViewModelBehavior;
using Zenject;

namespace Scripts.Main.Controllers
{
    public class GameController : ControllersBase
    {
        private IGameOverPanelBehaviour _gameOverPanelBehaviour;
        private ILoadScene _loadScene;

        [Inject]
        private void Construct(/*IGameOverPanelBehaviour gameOverPanelBehaviour, ILoadScene loadScene, */
            ILoadGameSettings loadGameSettings)
        {
            //_gameOverPanelBehaviour = gameOverPanelBehaviour;
            //_loadScene = loadScene;
            RuntimeSharedData.GameSettings = loadGameSettings.LoadGameSettings().Load(runAsync: false).Result;
        }

        public override void Init()
        {
            base.Init();
           // _gameOverPanelBehaviour.OnRestartButtonPressed = RestartGame; // TODO Move to levelController
        }

        private void RestartGame()
        {
            _loadScene.LoadScene(0);
        }
    }
}