using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class GameEnd : StateBase {

        [SerializeField] private Text gameEndText;
        [SerializeField] private Button newGameButton;

        public override void StartState()
        {
            ToggleUiElements(true);
        }

        public override void EndState()
        {
            ToggleUiElements(false);
        }

        private void ToggleUiElements(bool on)
        {
            gameEndText.gameObject.SetActive(on);
            newGameButton.gameObject.SetActive(on);
        }
    }
}
