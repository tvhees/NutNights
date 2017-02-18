namespace GameStates
{
    public class ResetGame : StateBase
    {
        public override void StartState()
        {
            Game.ResetGame();
            NextState();
        }
    }
}