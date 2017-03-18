using UI;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "TutorialController.asset", menuName = "Controllers/Tutorial")]
    public class TutorialController : Manager
    {
        private TutorialMessage message;

        public void SetMessageObject(TutorialMessage message)
        {
            this.message = message;
        }

        public void DisplayMessage(string text, Vector2 position)
        {
            message.Display(text, position);
        }

        public void HideMessage()
        {
            message.Hide();
        }
    }
}