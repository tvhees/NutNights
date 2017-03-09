using System;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "DoorsController.asset", menuName = "Controllers/Doors")]
    public class DoorsController : CollectionController
    {
        public int DoorsAcquired {
            get { return Cards.Count; }
        }

        public new void OnGameStart(params Collection[] dependencies)
        {
            // Left empty
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