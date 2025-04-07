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
        Debug.Log("���μ��� ����");
        currentStageProgress = 0;
        while (frameworkState==eStageFrameworkState.InProgress)
        {
            ++currentStageProgress;
            if (currentStageProgress >= GameConst.RaceStageCount)
            {
                Debug.Log("�¸�");
                frameworkState = eStageFrameworkState.Victory;
                break;
            }
            await SubProcessAsync();
        }
    }
    async UniTask SubProcessAsync()
    {
        Debug.Log(" ���� ���μ��� ����");

        ////�÷��̾��� ��ġ�κ��� 5�� ������ ���� ����
        //Vector2 playerPosition = Vector2.zero;//Player.PlayerCharacter.Transform.position;
        //Vector2 spawnPosition = playerPosition + Vector2.left * 5f;

        ////�÷��̾ �ش� ��ġ�� �����ϱ���� ���
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
        ////����

        ////���� ĳ���Ϳ� ��ȯ�� ������ ������ ���Ѵ� .
        await UniTask.WaitForSeconds(1f);
        Debug.Log("���� ��ȯ");
    }
    //��� ��ȯ�ɰ��̴� .
    async UniTask LoopPorccessAsync()
    {
        while (frameworkState == eStageFrameworkState.InProgress)
        {
            await SubProcessAsync();
        }
    }
}
