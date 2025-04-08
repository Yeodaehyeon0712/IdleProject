using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMComponent :FSMComponent
{
    public EnemyFSMComponent(Actor owner) : base(owner) { }
    protected override void GenerateFSMState()
    {
        fsmDictionary.Add(eFSMState.Idle, new IdleState(owner));
        fsmDictionary.Add(eFSMState.Battle, new IdleState(owner));
        fsmDictionary.Add(eFSMState.Death, new IdleState(owner));
    }
}
