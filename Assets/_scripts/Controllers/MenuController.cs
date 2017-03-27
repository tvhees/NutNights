using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    [CreateAssetMenu(fileName = "MenuController.asset", menuName = "Controllers/Menu")]
    public class MenuController : Manager
    {
        public void StartNewGame()
        {
            SceneManager.LoadSceneAsync("Game");
        }

        public void StartTutorialGame()
        {
            SceneManager.LoadSceneAsync("Tutorial");
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}