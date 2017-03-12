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
        Button GetButton(int index);
        Button AddButton(int index);
        Button AddButton(Button button, int index = 0);
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

        public Button GetButton(int index)
        {
            return transform.GetChild(index).GetComponent<Button>();
        }

        public virtual Button AddButton(int index)
        {
            var button = game.CreateCardButton(transform);
            return AddButton(button, index);
        }

        public virtual Button AddButton(Button button, int index = 0)
        {
            button.transform.SetParent(this.transform);
            button.transform.SetSiblingIndex(index);
            return button;
        }

        public void RemoveButton(int index)
        {
            game.ReturnCardButton(transform.GetChild(index).GetComponent<Button>());
        }

        public abstract void UpdateView(List<Card> cards);
    }
}