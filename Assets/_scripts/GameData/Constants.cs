using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public static class Constants
    {
        public const int handSize = 5;
        public const int prophecySize = 5;
        public const int nightmareSize = 5;
        public const int animationSpeed = 1;
        [System.Serializable]
        public enum CardType { Acorn = 6, Hickory = 4, Almond = 3, Nightmare = 10, Squirrel = 2 }
        public static List<Color> colors = new List<Color>{ Color.yellow, Color.green, Color.blue, Color.red };
        static CardType[] suitTypes = { CardType.Squirrel, CardType.Almond, CardType.Hickory, CardType.Acorn };

        public static List<Card> CreateStartingDeck()
        {
            var cards = new List<Card>();
            for(int i = 0; i < colors.Count; i++)
            {
                for(int j = 0; j < suitTypes.Length; j++)
                {
                    for (int k = 0; k < (int)suitTypes[j]; k++)
                        cards.Add(new Card(colors[i], suitTypes[j]));
                }

                // Add additional suns to each colour
                for (int j = 0; j < i; j++)
                {
                    cards.Add(new Card(colors[i], CardType.Acorn));
                }

            }

            for (int i = 0; i < (int)CardType.Nightmare; i++)
            {
                cards.Add(Card.Nightmare);
            }
            return cards;
        }
    }
}