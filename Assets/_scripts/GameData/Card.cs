using UnityEngine;

namespace GameData
{
    [System.Serializable]
    public struct Card
    {

        public Color color;
        public Constants.CardType type;

        public Card(Color color, Constants.CardType type)
        {
            this.color = color;
            this.type = type;
        }

        public static Card Door(Color color)
        {
            return new Card(color, Constants.CardType.Squirrel);
        }

        public static Card Key(Color color)
        {
            return new Card(color, Constants.CardType.Almond);
        }

        public static Card Nightmare { get { return new Card(Color.black, Constants.CardType.Nightmare); } }
    }
}