using Controllers;
using Scripts.Main.Settings;
using Zenject;

namespace Scripts.Main.Controllers
{
    public class GameController : ControllersBase
    {
        [Inject]
        private void Construct(ILoadGameSettings loadGameSettings)
        {
            RuntimeSharedData.GameSettings = loadGameSettings.LoadGameSettings().Load(runAsync: false).Result;
        }
    }
}