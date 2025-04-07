using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    #region Variablels
    public int CurrentStageProgress => currentStageProgress;
    int currentStageProgress;
    #endregion
    protected override async UniTask ProcessFrameworkAsync(CancellationToken token)
    {

        switch (Data.Type)
        {
            case eStageType.Race:
                await RacePorccessAsync();
                break;
            case eStageType.Boss:
                break;
            case eStageType.Loop:
                await LoopPorccessAsync();
                break;
        }
    }
    async UniTask RacePorccessAsync()
    {
        Debug.Log("프로세스 시작");
        currentStageProgress = 0;
        while (frameworkState==eStageFrameworkState.InProgress)
        {
            ++currentStageProgress;
            if (currentStageProgress >= GameConst.RaceStageCount)
            {
                Debug.Log("승리");
                frameworkState = eStageFrameworkState.Victory;
                break;
            }
            await SubProcessAsync();
        }
    }
    async UniTask SubProcessAsync()
    {
        Debug.Log(" 서브 프로세스 시작");

        ////플레이어의 위치로부터 5가 떨어진 곳을 설정
        //Vector2 playerPosition = Vector2.zero;//Player.PlayerCharacter.Transform.position;
        //Vector2 spawnPosition = playerPosition + Vector2.left * 5f;

        ////플레이어가 해당 위치에 도달하기까지 대기
        //while ((spawnPosition - playerPosition).sqrMagnitude < 15*15)
        //{
        //    //if (Player.PlayerCharacter.FSMState == eFSMState.Death)
        //    //{
        //    //    _currentContentsResultState = eContentResultState.Defeat;
        //    //    yield break;
        //    //}
        //    //playerPosition = Player.PlayerCharacter.Transform.position;
        //    await UniTask.Yield();
        //}
        ////스폰

        ////이후 캐릭터와 소환된 적들의 죽음을 비교한다 .
        await UniTask.WaitForSeconds(1f);
        Debug.Log("몬스터 소환");
    }
    //계속 소환될것이다 .
    async UniTask LoopPorccessAsync()
    {
        while (frameworkState == eStageFrameworkState.InProgress)
        {
            await SubProcessAsync();
        }
    }
}
