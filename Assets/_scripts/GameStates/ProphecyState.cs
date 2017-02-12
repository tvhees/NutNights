using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class ProphecyStateBase : StateBase {

        [SerializeField] private Image interactionShield;

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
        }

    }
}
