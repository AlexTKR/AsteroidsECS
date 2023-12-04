using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Scripts.Main.Controllers
{
    public interface ISceneLoader
    {
        UniTask LoadScene(int sceneId);
    }

    public class SceneLoaderController : ControllersBase , ISceneLoader
    {
        public async UniTask LoadScene(int targetIndex)
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            await SceneManager.LoadSceneAsync(targetIndex, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetIndex));
            await SceneManager.UnloadSceneAsync(currentScene);
        }
        
    }
}