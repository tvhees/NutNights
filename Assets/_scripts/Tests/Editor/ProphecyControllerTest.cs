using System;
using Collections;
using Controllers;
using GameData;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests
{
    [TestFixture]
    public class ProphecyControllerTest
    {
        private ProphecyController prophecy;

        public T GetControllerMockup<T>() where T : CollectionController
        {
            var controller = ScriptableObject.CreateInstance<T>();
            var obj = NSubstitute.Substitute.For<ICollectionObject>();
            controller.SetCollectionObject(obj);
            return controller;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            prophecy = GetControllerMockup<ProphecyController>();
            prophecy.OnGameStart();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(prophecy);
        }

        [Test]
        public void GetDoorFromProphecy()
        {
            var testColor = Color.red;
            var failColor = Color.blue;
            var doors = GetControllerMockup<DoorsController>();

            prophecy.AddCard(Card.Door(testColor));
            Assert.Throws<ArgumentOutOfRangeException>(() => doors.GetDoorFrom(prophecy, failColor));

            for (var i = 0; i < (int) Constants.CardType.Squirrel; i++)
            {
                Assert.False(doors.ObtainedAll(testColor));
                Assert.DoesNotThrow(() => doors.GetDoorFrom(prophecy, testColor));
                prophecy.AddCard(Card.Door(testColor));
            }
            Assert.True(doors.ObtainedAll(testColor));
            Object.DestroyImmediate(doors);
        }
    }
}