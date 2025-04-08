using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyFSMComponent :FSMComponent
{
    public EnemyFSMComponent(Actor owner) : base(owner) { }
    public override void GenerateFSMState()
    {
        fsmDictionary.Add(eFSMState.Idle, new EnemyIdleState(owner));
        fsmDictionary.Add(eFSMState.Battle, new BattleState(owner));
        fsmDictionary.Add(eFSMState.Death, new DeathState(owner));
    }
}
