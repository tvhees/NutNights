﻿using System.Linq;
using GameStates;
using UnityEngine;

namespace Controllers
{
    public enum States { ResetGame, NewGame, Cleanup, WaitingForPlayer, Door, Prophecy, CompleteProphecy, Nightmare, ProcessingTurn, CheckingWinLoss, GameWon, GameLost }

    [CreateAssetMenu(fileName = "StateController.asset", menuName = "Controllers/State")]
    public class StateController : Manager
    {
        private State stateObject;
        private StateBase current;
        private StateBase[] allStates;
        private GameController gameController;

        public bool IsInCleanup { get { return current == allStates[(int)States.Cleanup]; } }
        public bool IsInProphecy { get { return current == allStates[(int) States.Prophecy]; } }

        public void SetStateObject(State stateObject)
        {
            this.stateObject = stateObject;
            allStates = stateObject.GetComponentsInChildren<StateBase>(true);
            current = allStates.First();
        }

        public void SetGameController(GameController gameController)
        {
            this.gameController = gameController;
        }

        public void MoveToNext()
        {
            var currentIndex = current.transform.GetSiblingIndex();
            var newIndex = (int)Mathf.Repeat(currentIndex + 1, stateObject.transform.childCount);
            MoveToState(allStates[newIndex]);
        }

        public void MoveToState(States newState)
        {
            MoveToState(allStates[(int)newState]);
        }

        private void MoveToState(StateBase newState)
        {
            current.EndState();
            current = newState;
            current.gameObject.SetActive(true);
            current.StartState();
        }
    }
}