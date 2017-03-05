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
        Button AddButton(int index);
        void RemoveButton(int index);
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

        public abstract Button AddButton(int index);

        public void RemoveButton(int index)
        {
            game.ReturnCardButton(transform.GetChild(index).GetComponent<Button>());
        }

        public abstract void UpdateView(List<Card> cards);
    }
}