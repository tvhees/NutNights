using Controllers;

namespace GameStates
{
    public class CheckingWinLoss : StateBase
    {
        public override void StartState()
        {
            if (Game.Won)
                StateController.MoveToState(States.GameWon);
            else if (Game.Lost)
                StateController.MoveToState(States.GameLost);
            else
                NextState();
        }

        protected override void NextState()
        {
            StateController.MoveToState(States.Cleanup);
        }
    }
}