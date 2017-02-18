using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour {

        public void StartNewGame()
        {
            SceneManager.LoadSceneAsync("Game");
        }

        public void StartTutorialGame()
        {
            SceneManager.LoadSceneAsync("Tutorial");
        }
    }
}
