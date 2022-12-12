using UnityEngine.SceneManagement;

namespace Scripts.Main.Controllers
{
    public interface ILoadScene
    {
        void LoadScene(int sceneId);
    }

    public class SceneController : ControllersBase ,ILoadScene
    {
        public void LoadScene(int targetIndex)
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            var loadSceneAsync = SceneManager.LoadSceneAsync(targetIndex, LoadSceneMode.Additive);

            loadSceneAsync.completed += loadOperation =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetIndex));
                var unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            };
        }
        
    }
}