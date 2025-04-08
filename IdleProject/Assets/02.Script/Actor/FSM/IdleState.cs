using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :BaseState
{
    #region State Method
    public IdleState(Actor owner) : base(owner) { }
    public override void OnStateEnter()
    {
        owner.Skin.SetAnimationInt(eCharacterAnimState.Idle);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateStay(float deltaTime)
    {
        
    }
    #endregion
}
