using Collections;
using Controllers;
using GameData;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class HandControllerTest
    {
        private HandController hand;

        [SetUp]
        public void Setup()
        {
            hand = ScriptableObject.CreateInstance<HandController>();
            var handObj = NSubstitute.Substitute.For<ICollectionObject>();
            hand.SetCollectionObject(handObj);
            hand.OnGameStart();
            Debug.Log("Setup: " + hand.Size);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(hand);
        }

        [Test]
        public void CardsAddedInCorrectSequence()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                Assert.IsTrue(hand.LowestEmptyIndex == i);
                hand.AddCard(new Card());
            }
            Debug.Log(hand.Size);
        }

        [Test]
        public void EmptyIndicesMatchRemovedCard()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                hand.AddCard(new Card());
            }
            var target = NSubstitute.Substitute.For<ICollectionController>();
            hand.MoveCardTo(target, 2);
            hand.MoveCardTo(target, 3);
            Debug.Log(hand.Size);
            Assert.IsTrue(hand.LowestEmptyIndex == 2);
            Assert.IsTrue(hand.Size == Constants.handSize - 2);
        }

        [Test]
        public void MovingAllCardsCreatesCorrectEmptyIndexArray()
        {
            for(var i = 0; i < Constants.handSize; i++)
            {
                hand.AddCard(new Card());
            }
            Assert.IsTrue(hand.LowestEmptyIndex == -1);

            var target = NSubstitute.Substitute.For<ICollectionController>();
            hand.MoveAllTo(target);
            Assert.IsTrue(hand.EmptyIndices.Count == Constants.handSize);
        }
    }
}