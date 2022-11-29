using Controllers;
using Scripts.ECS.World;
using Scripts.Main.Settings;
using Scripts.ViewViewModelBehavior;

namespace Scripts.Main.Controllers
{
    public class GameController : ControllersBase
    {
        private IGameOverPanelBehaviour _gameOverPanelBehaviour;
        private ILoadScene _loadScene;

        public override void Init(IGetControllers getControllers)
        {
            base.Init(getControllers);
            _gameOverPanelBehaviour = getControllers.GetBehavior<IGameOverPanelBehaviour>();
            _loadScene = getControllers.GetBehavior<ILoadScene>();

            _gameOverPanelBehaviour.OnRestartButtonPressed = RestartGame;
            RuntimeSharedData.GameSettings = getControllers.GetBehavior<ILoadGameSettings>().LoadGameSettings()
                .Load(runAsync: false).Result;
        }

        private void RestartGame()
        {
            _loadScene.LoadScene(0);
        }
    }
}