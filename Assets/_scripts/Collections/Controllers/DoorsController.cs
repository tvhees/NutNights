using UnityEngine;

namespace Collections.Controllers
{
    [CreateAssetMenu(fileName = "DoorsController.asset", menuName = "Controllers/Doors")]
    public class DoorsController : CollectionController
    {
        public int DoorsAcquired {
            get { return Cards.Count; }
        }

        public override void OnGameStart(params Collection[] dependencies)
        {

        }

        public void GetDoorFrom(CollectionController source, Color color)
        {
            if (ObtainedAll(color))
                return;
            source.MoveCardTo(this, Card.Door(color));
            source.Shuffle();
        }

        public bool ObtainedAll(Color color)
        {
            var doorsObtained = 0;
            for (var i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].color.Equals(color)) doorsObtained++;
            }
            return doorsObtained >= (int)Constants.CardType.Squirrel;
        }
    }
}