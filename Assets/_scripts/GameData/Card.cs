using JetBrains.Annotations;
using UnityEngine;

namespace GameData
{
    [System.Serializable]
    public struct Card
    {
        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Card c1, Card c2)
        {
            return !(c1 == c2);
        }

        public bool Equals(Card other)
        {
            return color.Equals(other.color) && type == other.type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Card && Equals((Card) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (color.GetHashCode() * 397) ^ (int) type;
            }
        }

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