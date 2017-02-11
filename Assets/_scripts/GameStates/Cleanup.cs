namespace GameStates
{
    public class Cleanup : StateBase {

        public override void StartState()
        {
            Game.DrawCardRecursive();

            // For the first refill of a new game or after discarding hand to a nightmare we want this flag to be true
            // This prevents triggering of door and nightmare effects for large hand refills.
            Flags.recycleDreamsAndDoors = false;
        }
    }
}
