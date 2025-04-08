using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState :BaseState
{
    #region Fields
    //추후 수정
    float _currentRange=5f;
    #endregion

    #region State Method
    public MoveState(Actor owner) : base(owner) 
    {
        var tool = fsm.ActorSearchingTool;
        tool.AddConditionCharacterType(eActorType.Enemy).AddConditionFSMState(~eFSMState.Death)
            .AddConditionDistance(owner, _currentRange * _currentRange);
    }
    public override void OnStateEnter()
    {
        owner.Skin.SetAnimationInt(eCharacterAnimState.Move);
    }
    public override void OnStateStay(float deltaTime)
    {
        //OnCheckRange();
        var target = fsm.Target;

        if (target != null)
        {
            if (target.FSMState == eFSMState.Death)
            {
                fsm.Target = null; return;
            }

            if (Mathf.Abs(owner.transform.position.x - fsm.Target.transform.position.x) > _currentRange)
                OnMove(deltaTime);
            else
                fsm.State = eFSMState.Battle;
        }
        else
        {
            if (SearchTarget() == false)
                OnMove(deltaTime);
        }
    }
    public override void OnStateExit()
    {

    }
    #endregion

    #region Move Method
    void OnMove(float deltaTime)
    {
        var moveSpeed = owner.Status.GetStatus(eStatusType.MoveSpeed);
        owner.transform.Translate(moveSpeed * Vector3.right * deltaTime , Space.Self);
    }
    //void OnCheckRange()
    //{
    //    if (_currentRange == Player.GetAttackRange()) return;

    //    _currentRange = Player.GetAttackRange();
    //    fsm.ActorSearchingTool.AddConditionDistance(owner, _currentRange * _currentRange);
    //}
    #endregion

    #region Search Method
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
    #endregion
}
