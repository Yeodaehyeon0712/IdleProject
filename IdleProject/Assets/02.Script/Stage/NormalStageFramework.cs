using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    #region Variablels
    public int CurrentStageProgress => currentStageProgress;
    int currentStageProgress;
    #endregion

    #region Framework Method
    protected override async UniTask ProcessFrameworkAsync(CancellationToken token)
    {

        switch (Data.Type)
        {
            case eStageType.Race:
                await RacePorccessAsync(token);
                break;
            case eStageType.Boss:
                await BossPorccessAsync(token);
                break;
            case eStageType.Loop:
                await LoopPorccessAsync(token);
                break;
        }
    }
    #endregion

    #region Normal Stage Method
    async UniTask BossPorccessAsync(CancellationToken token)
    {
        while (frameworkState == eStageFrameworkState.InProgress)
        {
            await SubProcessAsync(token);
            frameworkState = eStageFrameworkState.Victory;
        }
    }
    async UniTask RacePorccessAsync(CancellationToken token)
    {
        Debug.Log("프로세스 시작");
        currentStageProgress = 0;
        UIManager.Instance.Stage.GetRaceUI.SetProgress(0);
        while (frameworkState==eStageFrameworkState.InProgress)
        {
            ++currentStageProgress;

            if (currentStageProgress > GameConst.RaceStageCount)
            {
                Debug.Log("승리");
                frameworkState = eStageFrameworkState.Victory;
                break;
            }
            await SubProcessAsync(token);
            var value = (float)currentStageProgress / GameConst.RaceStageCount;
            UIManager.Instance.Stage.GetRaceUI.SetProgress(value);
        }
        UIManager.Instance.Stage.GetRaceUI.SetProgress(1f);
    }
    async UniTask LoopPorccessAsync(CancellationToken token)
    {
        while (frameworkState == eStageFrameworkState.InProgress)
        {
            await SubProcessAsync(token);
        }
    }
    #endregion

    #region SubProcessMethod
    async UniTask SubProcessAsync(CancellationToken token)
    {
        await WaitSpawnableDistanceAsync(token);
        await SpawnGroupAsync(token);
        await WaitAllEnemiesDeadAsync(token);
    }
    async UniTask WaitSpawnableDistanceAsync(CancellationToken token)
    {
        Transform playerTransform = Player.PlayerCharacter.transform;
        float requiredDistanceSqr = GameConst.SpawnIntervalDistanceSqr;
        Vector3 startPosition = playerTransform.position;

        while (true)
        {
            float movedDistanceSqr = (playerTransform.position - startPosition).sqrMagnitude;
            if (movedDistanceSqr >= requiredDistanceSqr)
                break;

            await UniTask.Yield(token);
        }
    }
    Actor[] enemyList;
    async UniTask SpawnGroupAsync(CancellationToken token)
    {
        var stageData = DataManager.StageTable[stageIndex];
        var spawnTasks = new List<UniTask<Enemy>>();
        enemyList = new Actor[stageData.EnemyCount];

        for (int i = 0; i < stageData.EnemyCount; i++)
        {
            int selectRandom = Random.Range(0, stageData.EnemyIndexArr.Length);
            var positioin = Player.PlayerCharacter.transform.position+ (Vector3.right *i)+(Vector3.right*5);
            spawnTasks.Add(ActorManager.Instance.SpawnEnemy(stageData.EnemyIndexArr[selectRandom],positioin));
        }
        enemyList = await UniTask.WhenAll(spawnTasks);
    }
    //To Do :: 추후 수정 필요
    async UniTask WaitAllEnemiesDeadAsync(CancellationToken token)
    {
        while (true)
        {
            bool allDead = true;
            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i].FSMState != eFSMState.Death)
                {
                    allDead = false;
                    break;
                }
            }
            if (allDead)
                break;
            await UniTask.Yield();
        }
    }
    #endregion
}
