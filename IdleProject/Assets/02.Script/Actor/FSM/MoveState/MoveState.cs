using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState :BaseState
{
    #region State Method
    public MoveState(Actor owner) : base(owner) { }
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {

    }
    public override void OnStateExit()
    {

    }
    #endregion

    #region Move Method
    void OnCheckRange()
    {
        //if (_currentRange != Player.GetAttackRange())
        //{
        //    _currentRange = Player.GetAttackRange();

        //    _controller.ActorSearchingTool.AddConditionDistance(_owner, _currentRange * _currentRange);
        //}

        //if (_currentSpecialRange != Player.GetSpeciaAttackRange())
        //{
        //    _currentSpecialRange = Player.GetSpeciaAttackRange();

        //    _controller.ActorSpecialSearchingTool.AddConditionDistance(_owner, _currentSpecialRange * _currentSpecialRange);
        //}
    }
    #endregion
}
