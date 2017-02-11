using System;
using UnityEngine;

namespace Collections.Controllers
{
    [CreateAssetMenu(fileName = "DeckController.asset", menuName = "Controllers/Deck")]
    public class DeckController : CollectionController
    {
        public override void OnGameStart(params Collection[] dependencies)
        {
            ResetCollection();
            NewDeck();
        }

        private void NewDeck()
        {
            Cards = Constants.CreateStartingDeck();
            Cards.Shuffle();
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
                        MoveCardTo(target);
                        break;
                    case Constants.CardType.Squirrel:
                        MoveCardTo(limbo);
                        break;
                    case Constants.CardType.Nightmare:
                        MoveCardTo(limbo);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}