using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterFSMComponent : FSMComponent
{
    public CharacterFSMComponent(Actor owner) : base(owner) { }
    public override void GenerateFSMState()
    {
        fsmDictionary.Add(eFSMState.Idle, new IdleState(owner));
        fsmDictionary.Add(eFSMState.Move, new MoveState(owner));
        fsmDictionary.Add(eFSMState.Battle, new BattleState(owner));
        fsmDictionary.Add(eFSMState.Death, new DeathState(owner));
    }
}
