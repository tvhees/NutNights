using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "LabyrinthController.asset", menuName = "Controllers/Labyrinth")]
    public class LabyrinthController : CollectionController
    {
        public Constants.CardType LastType { get { return Cards.Count > 0 ? Cards.GetLast().type : Constants.CardType.Nightmare; } }
        public Color LastColor { get { return Cards.Count > 0 ? Cards.GetLast().color : Color.black; } }
        public List<Color> LastThree;

        public override void OnGameStart(params Collection[] dependencies)
        {
            base.OnGameStart();
            LastThree = new List<Color> { Color.black };
        }

        public bool TripletCompleted()
        {
            if (LastThree.Count >= 3 || !LastColor.Equals(LastThree.GetLast()))
                LastThree.Clear();

            LastThree.Add(LastColor);
            return LastThree.Count >= 3;
        }
    }
}