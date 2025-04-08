using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : BaseState
{
    #region Fields
    float defaultAttackElapsedTime;
    #endregion

    #region State Method
    public BattleState(Actor owner) : base(owner) { }
    public override void OnStateEnter()
    {
        defaultAttackElapsedTime = 1f;
    }
    public override void OnStateStay(float deltaTime)
    {
        var target = fsm.Target;

        if (target != null)
        {
            if (target.FSMState == eFSMState.Death)
            {
                fsm.Target = null; return;
            }

            defaultAttackElapsedTime += deltaTime;

            if (defaultAttackElapsedTime > 1f)
            {
                defaultAttackElapsedTime = 0;
                owner.DefaultAttack();               
                return;
            }
        }
        else
        {
            if (SearchTarget() == false)
                fsm.State = eFSMState.Move;
        }
    }
    public override void OnStateExit()
    {

    }
    #endregion

    bool SearchTarget()
    {
        Actor actor = ActorManager.Instance.FindActor(fsm.ActorSearchingTool);
        if (actor != null)
        {
            fsm.Target = actor;
            return true;
        }
        return false;
    }
    void DefaultAttack()
    {

    }
    void SkillAttack() 
    {
       // owner.DefaultAttackElapsedTime = 0f;
    }
}
