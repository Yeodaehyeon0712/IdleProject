using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState :IdleState
{
    public EnemyIdleState(Actor owner) : base(owner) 
    {
        var tool = fsm.ActorSearchingTool;
        tool.AddConditionCharacterType(eActorType.Character).AddConditionFSMState(~eFSMState.Death)
            .AddConditionDistance(owner, GameConst.BattleDistanceSqr);
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

            float sqrDistance = (owner.transform.position - fsm.Target.transform.position).sqrMagnitude;
            if (sqrDistance <= GameConst.BattleDistanceSqr)
                fsm.State = eFSMState.Battle;
        }
        else
        {
            SearchTarget();
        }

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
    }
}
