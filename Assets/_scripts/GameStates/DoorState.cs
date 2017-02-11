using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class DoorState : StateBase {

        [SerializeField] private Image interactionShield;
        [SerializeField] private Button completeProphecy;

        public override void StartState()
        {
            ToggleState(true);
        }

        public override void EndState()
        {
            ToggleState(false);
        }

        private void ToggleState(bool on)
        {
            interactionShield.enabled = !on;
            completeProphecy.gameObject.SetActive(on);
            Flags.doorDrawn = on;
        }

    }
}
