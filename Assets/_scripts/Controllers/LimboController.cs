using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "LimboController.asset", menuName = "Controllers/Limbo")]
    public class LimboController : CollectionController
    {
        public void EmptyTo(CollectionController target)
        {
            if (IsEmpty || target.IsEmpty) return;

            while (!IsEmpty)
                MoveCardTo(target);

            target.Shuffle();
        }
    }
}