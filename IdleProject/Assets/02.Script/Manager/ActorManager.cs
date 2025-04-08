using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : TSingletonMono<ActorManager>
{
    #region Fields
    //Factory Fields
    ActorFactory actorFactory;

    //Attack Handler Fields
    Queue<AttackHandler> attackHandlerQueue = new Queue<AttackHandler>(100);
    public AttackHandler PushAttackHandler
    {
        set => attackHandlerQueue.Enqueue(value);
    }
    #endregion

    #region Actor Manager Method
    protected override void OnInitialize()
    {
        actorFactory = new ActorFactory(transform);
        IsLoad = true;
    }
    private void Update()
    {
        if (TimeManager.Instance.IsActiveTimeFlow == false) return;
        OnUpdateAttackHandler();
    }
    #endregion

    #region Spawn Method
    public async UniTask<Character> SpawnCharacter(int objectID, Vector3 position) => await actorFactory.SpawnObjectAsync(eActorType.Character, objectID, position) as Character;
    public async UniTask<Enemy> SpawnEnemy(int objectID, Vector3 position)
    {   
        var enemy= await actorFactory.SpawnObjectAsync(eActorType.Enemy, objectID, position) as Enemy;
        //enemy.ActiveActor();
        return enemy;
    }
    public void RegisterActorPool(uint worldID, eActorType type, int pathHash) => actorFactory.RegisterToObjectPool(worldID, type, pathHash);
    public Dictionary<uint, Actor> GetSpawnedActors => actorFactory.GetSpawnedObjects;
    public void CleanAll()
    {
        actorFactory.Clear();
    }
    #endregion

    #region Attack Handler Method
    void OnUpdateAttackHandler()
    {
        while (attackHandlerQueue.Count > 0)
        {
            AttackHandler attackHandler = attackHandlerQueue.Dequeue();
            if (GetSpawnedActors.ContainsKey(attackHandler.TargetID)==false)
                continue;

            if (GetSpawnedActors[attackHandler.TargetID].FSMState != eFSMState.Death)
            {
                if (attackHandler.Damage >= 0)
                    GetSpawnedActors[attackHandler.TargetID].Hit(in attackHandler);
                else
                    GetSpawnedActors[attackHandler.TargetID].Recovery(in attackHandler);
            }
        }
    }
    #endregion

    #region Searching Method
    public Actor FindActor(ActorSearchingTool tool)
    {
        foreach (var actor in GetSpawnedActors.Values)
        {
            if (tool.CheckCondition(actor))
                return actor;
        }
        return null;
    }
    #endregion
}
