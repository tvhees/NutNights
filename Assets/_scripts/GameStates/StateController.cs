using System;
using System.Collections;
using System.Linq;
using GameStates;
using RSG;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Assertions.Must;

public enum States { NewGame, Cleanup, WaitingForPlayer, Door, Prophecy, CompleteProphecy, Nightmare, ProcessingTurn, CheckingWinLoss, GameWon, GameLost }

[CreateAssetMenu(fileName = "StateController.asset", menuName = "Controllers/State")]
[ManagerDependency(typeof(GameController))]
public class StateController : Manager
{
    private State stateObject;
    private StateBase current;
    private StateBase[] allStates;
    private GameController gameController;

    public bool IsInCleanup { get { return current == allStates[(int)States.Cleanup]; } }

    public void SetStateObject(State stateObject)
    {
        this.stateObject = stateObject;
        allStates = stateObject.GetComponentsInChildren<StateBase>(true);
        current = allStates.First();
        current.gameObject.SetActive(true);
        current.StartState();
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

    private void MoveToState(StateBase newStateBase)
    {
        current.EndState();
        current.gameObject.SetActive(false);
        current = newStateBase;
        current.gameObject.SetActive(true);
        current.StartState();
    }
}