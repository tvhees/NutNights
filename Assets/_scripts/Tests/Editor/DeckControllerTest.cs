using Collections;
using Controllers;
using GameData;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class DeckControllerTest
    {
        private DeckController deck;

        [TestFixtureSetUp]
        public void Setup()
        {
            deck = ScriptableObject.CreateInstance<DeckController>();
            var deckObj = NSubstitute.Substitute.For<ICollectionObject>();
            deck.SetCollectionObject(deckObj);
            deck.OnGameStart();
        }

        [Test]
        public void NewDeckIsNotEmpty()
        {
            Assert.IsFalse(deck.IsEmpty);
        }

        [Test]
        public void MoveCardDecreasesDeckSize()
        {
            var target = NSubstitute.Substitute.For<ICollectionController>();
            var size = deck.Size;
            deck.MoveCardTo(target);
            Assert.IsTrue(deck.Size == size - 1);
        }

        [Test]
        public void AddCardIncreasesDeckSize()
        {
            var card = new Card();
            var size = deck.Size;
            deck.AddCard(card);
            Assert.IsTrue(deck.Size == size + 1);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(deck);
        }
    }
}
