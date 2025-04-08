using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    #region Variable
    protected Actor owner;
    protected FSMComponent fsm;
    #endregion

    #region State Method
    public BaseState(Actor owner)
    {
        this.owner = owner;
        this.fsm = owner.FSM;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateStay(float deltaTime);
    public abstract void OnStateExit();
    public virtual void Clear()
    {

    }
    #endregion
}
