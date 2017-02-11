namespace GameStates
{
    public class NewGame : StateBase
    {
        public override void StartState()
        {
            Flags.recycleDreamsAndDoors = true;
            Game.StartGame();
            NextState();
        }
    }
}