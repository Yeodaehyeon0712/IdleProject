using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActorSearchingTool 
{
    #region Fields
    eActorFinderAttribute attribute;
    eActorType targetActorType;
    eFSMState targetFsmState;
    Actor distancePivotActor;
    float sqrDistance;
    HashSet<uint> cullingIDHash = new HashSet<uint>();
    #endregion

    #region Searching Method
    //Actor Type
    public ActorSearchingTool AddConditionCharacterType(eActorType actorType)
    {
        attribute |= eActorFinderAttribute.CharacterType;
        this.targetActorType = actorType;
        return this;
    }
    //Distance
    public ActorSearchingTool AddConditionDistance(Actor actor, float sqrDistance)
    {
        attribute |= eActorFinderAttribute.Distance;
        distancePivotActor = actor;
        this.sqrDistance = sqrDistance;
        return this;
    }
    //FSM State
    public ActorSearchingTool AddConditionFSMState(eFSMState state)
    {
        attribute |= eActorFinderAttribute.FSMState;
        targetFsmState = state;
        return this;
    }
    //Culling
    public ActorSearchingTool AddConditionCullingWorldID(uint worldID)
    {
        attribute |= eActorFinderAttribute.CullingWorldID;
        cullingIDHash.Add(worldID);
        return this;
    }
    //Remove
    public void RemoveCondition(eActorFinderAttribute attribute)
    {
        if ((attribute & attribute) == 0) return;

        this.attribute &= ~attribute;

        // 특정 조건 비활성화 시 추가 동작 처리
        if ((attribute & eActorFinderAttribute.CullingWorldID) != 0)
            cullingIDHash.Clear();
    }
    #endregion

    #region Check Method
    public bool CheckCondition(Actor targetActor)
    {
        if ((attribute & eActorFinderAttribute.FSMState) != 0)
        {
            if ((targetActor.FSMState & targetFsmState) == 0)
                return false;
        }

        if ((attribute & eActorFinderAttribute.CharacterType) != 0)
        {
            if (targetActor.Type != targetActorType)
                return false;
        }

        if ((attribute & eActorFinderAttribute.CullingWorldID) != 0)
        {
            if (cullingIDHash.Contains(targetActor.WorldID))
                return false;
        }

        if ((attribute & eActorFinderAttribute.Distance) != 0)
        {
            if ((distancePivotActor.transform.position - targetActor.transform.position).sqrMagnitude > sqrDistance)
                return false;
        }

        return true;
    }
    #endregion
}
