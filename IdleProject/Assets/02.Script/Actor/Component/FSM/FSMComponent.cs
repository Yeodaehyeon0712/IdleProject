using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class FSMComponent : BaseComponent
{
    #region Fields
    [SerializeField] protected Dictionary<eFSMState, BaseState> fsmDictionary = new Dictionary<eFSMState, BaseState>();
    [SerializeField] protected eFSMState currentState = eFSMState.Idle;
    public eFSMState State
    {
        get => currentState;
        set
        {
            if (currentState == value)
                return;

            fsmDictionary[currentState].OnStateExit();
            currentState = value;
            fsmDictionary[currentState].OnStateEnter();
        }
    }
    public ActorSearchingTool ActorSearchingTool => searchingTool;
    protected ActorSearchingTool searchingTool;
    #endregion

    #region Component Method
    public FSMComponent(Actor owner) : base(owner, eComponent.FSMComponent, true)
    {
        searchingTool = new ActorSearchingTool();
        fsmDictionary.Add(eFSMState.Idle, new IdleState(owner));
    }
    protected abstract void GenerateFSMState();
    protected override void OnComponentFixedUpdate(float fixedDeltaTime)
    {
        fsmDictionary[currentState].OnStateStay(fixedDeltaTime);
    }
    #endregion


}
