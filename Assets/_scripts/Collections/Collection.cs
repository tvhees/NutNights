using System.Collections.Generic;
using Controllers;
using GameData;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Collections
{
    public interface ICollectionObject
    {
        void UpdateView(List<Card> cards);
    }

    [ManagerDependency(typeof(ManagerContainer))]
    public abstract class Collection : BaseMonoBehaviour, ICollectionObject
    {
        [SerializeField] protected Game game;
        protected CollectionController Controller;

        protected override void Awake()
        {
            base.Awake();
            Controller.SetCollectionObject(this);
        }

        public void SwapCards(int i, int j)
        {
            Controller.SwapCards(i, j);
        }

        public abstract void UpdateView(List<Card> cards);
    }
}