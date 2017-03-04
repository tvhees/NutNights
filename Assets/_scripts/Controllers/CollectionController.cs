﻿using System.Collections.Generic;
using Collections;
using GameData;
using UnityEngine.Assertions;

namespace Controllers
{
    public interface ICollectionController
    {
        void AddCard(Card card);
        void InsertCard(Card card);
    }

    public abstract class CollectionController : Manager, ICollectionController
    {
        protected ICollectionObject CollectionObject;

        protected List<Card> Cards = new List<Card>();

        public bool IsEmpty
        {
            get { return Cards.Count <= 0; }
        }

        public int Size
        {
            get { return Cards.Count; }
        }

        public virtual void OnGameStart(params Collection[] dependencies)
        {
            Cards = new List<Card>();
            UpdateView();
        }

        public void SetCollectionObject(ICollectionObject collectionObject)
        {
            this.CollectionObject = collectionObject;
        }

        public void MoveCardTo(ICollectionController target, bool toFront = false)
        {
            MoveCardTo(target, 0, toFront);
        }

        public void MoveCardTo(ICollectionController target, int index, bool toFront = false)
        {
            var card = GetCard(index);
            if (toFront)
                target.InsertCard(card);
            else
                target.AddCard(card);
            Cards.Remove(card);
            UpdateView();
        }

        public void MoveCardTo(ICollectionController target, Card cardDef)
        {
            if (IsEmpty)
                return;
            var index = Cards.IndexOf(cardDef);
            Assert.IsFalse(index < 0, index + " " + cardDef.color + " " + cardDef.type);
            MoveCardTo(target, index);
        }

        public void MoveAllTo(ICollectionController target, bool toFront = false)
        {
            if ((CollectionController)target == this)
                return;

            while (!IsEmpty)
                MoveCardTo(target, toFront);
        }

        public void MoveCardsTo(ICollectionController target, int number)
        {
            if ((CollectionController)target == this)
                return;

            for (int i = 0; i < number; i++)
            {
                if (IsEmpty)
                    break;

                MoveCardTo(target);
            }
        }

        public bool HasCard(Card card)
        {
            return Cards.Contains(card);
        }

        public Card GetCard()
        {
            return GetCard(0);
        }

        public Card GetCard(int index)
        {
            return Cards[index];
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            UpdateView();
        }

        public void InsertCard(Card card)
        {
            Cards.Insert(0, card);
            UpdateView();
        }

        public void SwapCards(int i, int j)
        {
            var temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
            UpdateView();
        }

        public virtual void UpdateView()
        {
            CollectionObject.UpdateView(Cards);
        }

        public void Shuffle()
        {
            Cards.Shuffle();
        }
    }
}