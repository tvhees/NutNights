using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class ProcessingTurn : StateBase
    {
        [SerializeField] private Image interactionShield;

        public override void StartState()
        {
            interactionShield.enabled = true;
        }
    }
}
