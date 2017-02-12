using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class NightmareState : StateBase {

        [SerializeField] private Image interactionShield;
        [SerializeField] private Button discardHand;
        [SerializeField] private Button discardFromDeck;

        public override void StartState()
        {
            ToggleState(true);
        }

        public override void EndState()
        {
            base.EndState();
            ToggleState(false);
        }

        private void ToggleState(bool on)
        {
            interactionShield.enabled = !on;
            discardHand.gameObject.SetActive(on);
            discardFromDeck.gameObject.SetActive(on);
            Flags.isInNightmare = on;
        }

    }
}
