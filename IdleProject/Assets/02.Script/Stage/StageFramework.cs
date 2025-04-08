using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class StageFramework
{
    #region Fields
    //Stage Data
    protected Data.StageData Data => DataManager.StageTable[stageIndex];
    protected int stageIndex;

    protected CancellationTokenSource frameworkCTS;
    //Framework State
    public eStageFrameworkState CurrentFrameworkState 
    { 
        get => frameworkState; 
        set { frameworkState = value; OnStageChange();}
    }
    protected eStageFrameworkState frameworkState;
    void OnStageChange()
    {
        switch (frameworkState)
        {
            case eStageFrameworkState.None:
                break;
            case eStageFrameworkState.SetUp:
                break;
            case eStageFrameworkState.InProgress:
                break;
            case eStageFrameworkState.Victory:
                break;
            case eStageFrameworkState.Defeat:
                break;
            case eStageFrameworkState.Clean:
                break;
        }
    }
    #endregion

    #region Stage Process Method
    //���� ���������� ���� ����� �غ�
    public virtual async UniTask SetupFrameworkAsync(int stageIndex)
    {
        CurrentFrameworkState = eStageFrameworkState.SetUp;
        this.stageIndex = stageIndex;

        //UI
        UIManager.Instance.Stage.CurrentStage = Data.Type;
        //Background
        BackgroundManager.Instance.SetupBackground(CameraManager.Instance.GetCamera(eCameraType.MainCamera).transform, Data.BackgroundKey);
        //Actor
        var actor = await ActorManager.Instance.SpawnCharacter(1, Vector3.zero);
        Player.RegisterPlayer(actor);
        //Camera
        CameraManager.Instance.GetCamera<MainCamera>(eCameraType.MainCamera).SetActor=actor;
    }

    //���� ���������� �����ϴ� �ܰ�
    public async UniTask  StartFrameworkAsync(long stageIndex)
    {
        using (frameworkCTS = new CancellationTokenSource())
        {
            try
            {
                CurrentFrameworkState = eStageFrameworkState.InProgress;
                StartFramework();
                await ProcessFrameworkAsync(frameworkCTS.Token);
            }
            catch(System.OperationCanceledException)
            {
                CurrentFrameworkState = eStageFrameworkState.Defeat;
            }
            finally
            {
                //���â �����ֱ�
                //if (frameworkCTS.IsCancellationRequested == false)
                //    UIManager.Instance.ResultPopUpUI.Enable();
            }
        }
    }
    void StartFramework()
    {
        Player.ActivePlayer();
    }
    protected abstract UniTask ProcessFrameworkAsync(CancellationToken token);
    #endregion

    #region Stage Stop Method
    //���μ����� ���߰� ���â�� �����ش� .
    public void StopFramework(bool isDie=true)
    {
        if (frameworkState == eStageFrameworkState.InProgress)
            frameworkCTS.Cancel();

        StopFrameworkAsync(isDie).Forget();
    }
    //
    async UniTask StopFrameworkAsync(bool isDie)
    {
        await UniTask.WaitUntil(() => frameworkState != eStageFrameworkState.InProgress);
        OnStopFramework();

        if (isDie)
        // UIManager.Instance.ResultPopUpUI.Enable();
        // else
        {
            //�������� �������� ������ ���
            CurrentFrameworkState = eStageFrameworkState.Victory;
            CleanFramework();
        }
    }
    //���������� �ʱ�ȭ�� �����Ѵ�
    //Remove Dynamic Things
    protected virtual void OnStopFramework()
    {
        //Player
        //Player.UnRegisterPlayer();
        //ActorManager.Instance.Clear();
    }
    #endregion

    #region Stage Clean Method
    //Remove Static Things
    //�̰� ��ҵ��� ��� �ʱ�ȭ �ϰ� ���� ���������� ���ư��� ����
    public void CleanFramework()
    {
        //UIManager.Instance.GameUI.CloseUIByFlag(eUI.Controller | eUI.BattleState);
        ExitStage(1f).Forget();
    }
    async UniTask ExitStage(float time)
    {
        await UniTask.WaitForSeconds(time);

        if (CurrentFrameworkState == eStageFrameworkState.Victory)
            StageManager.Instance.SetupStage(eContentsType.Normal, Data.SuccessIndex);
        else
            StageManager.Instance.SetupStage(eContentsType.Normal, Data.FailedIndex);

        CurrentFrameworkState = eStageFrameworkState.Clean;
    }
    #endregion
}
