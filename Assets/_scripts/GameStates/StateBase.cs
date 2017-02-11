﻿using UnityEngine;

namespace GameStates
{
    public abstract class StateBase : BaseMonoBehaviour
    {
        protected GameController Game;
        protected StateController StateController;

        protected override void Awake()
        {
            Game = GetManager<GameController>();
            StateController = GetManager<StateController>();
        }

        public virtual void StartState()
        {
            Debug.Log(name);
        }

        public virtual void EndState()
        {
            gameObject.SetActive(false);
        }

        protected virtual void NextState()
        {
            StateController.MoveToNext();
        }
    }
}
