using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class WaitingForPlayer : StateBase
    {
        [SerializeField] private Image interactionShield;
        [SerializeField] private Toggle discardToggle;

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
            discardToggle.gameObject.SetActive(on);
        }
    }
}
