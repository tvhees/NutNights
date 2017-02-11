using System;
using UnityEngine;

namespace GameStates
{
    [ManagerDependency(typeof(ManagerContainer))]
    public class State : BaseMonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            GetManager<StateController>().SetStateObject(this);
        }
    }
}