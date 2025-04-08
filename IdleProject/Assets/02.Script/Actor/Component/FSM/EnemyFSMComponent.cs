using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyFSMComponent :FSMComponent
{
    public EnemyFSMComponent(Actor owner) : base(owner) { }
    public override void GenerateFSMState()
    {
        fsmDictionary.Add(eFSMState.Idle, new IdleState(owner));
        //fsmDictionary.Add(eFSMState.Battle, new IdleState(owner));
        //fsmDictionary.Add(eFSMState.Death, new IdleState(owner));
    }
}
