using GameData;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "ProphecyController.asset", menuName = "Controllers/Prophecy")]
    public class ProphecyController : CollectionController
    {
        public bool IsFull { get { return Cards.Count >= Constants.prophecySize; } }
    }
}