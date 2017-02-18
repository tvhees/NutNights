using Collections;
using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "ProphecyController.asset", menuName = "Controllers/Prophecy")]
    public class ProphecyController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.prophecySize; } }

        public override void OnGameStart(params Collection[] dependencies)
        {
            ((Prophecy)CollectionObject).AddButtons();
            base.OnGameStart(dependencies);
        }
    }
}