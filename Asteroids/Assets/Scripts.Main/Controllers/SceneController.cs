using UnityEngine.SceneManagement;

namespace Scripts.Main.Controllers
{
    public interface ISceneLoader
    {
        void LoadScene(int sceneId);
    }

    public class SceneLoaderController : ControllersBase , ISceneLoader
    {
        public void LoadScene(int targetIndex)
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            var loadSceneAsync = SceneManager.LoadSceneAsync(targetIndex, LoadSceneMode.Additive);

            loadSceneAsync.completed += loadOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetIndex));
                SceneManager.UnloadSceneAsync(currentScene);
            };
        }
        
    }
}