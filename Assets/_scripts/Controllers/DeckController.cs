using System;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "DeckController.asset", menuName = "Controllers/Deck")]
    public class DeckController : CollectionController
    {
        public override void OnGameStart(params Collection[] dependencies)
        {
            base.OnGameStart();
            NewDeck();
        }

        private void NewDeck()
        {
            Cards = Constants.CreateStartingDeck();
            Shuffle();
        }

        public void MoveCardsTo(CollectionController target, CollectionController limbo, int number)
        {
            if (target == this)
                return;

            for (var i = 0; i < number; i++)
            {
                if (IsEmpty)
                    break;

                switch (GetCard().type)
                {
                    case Constants.CardType.Acorn:
                    case Constants.CardType.Hickory:
                    case Constants.CardType.Almond:
                        MoveFirstCardTo(target);
                        break;
                    case Constants.CardType.Squirrel:
                        MoveFirstCardTo(limbo);
                        break;
                    case Constants.CardType.Nightmare:
                        MoveFirstCardTo(limbo);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}