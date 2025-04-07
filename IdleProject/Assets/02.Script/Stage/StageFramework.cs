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
        Debug.Log("���� ���������� ���� ����� �غ� ����");
        this.stageIndex = stageIndex;
        CurrentFrameworkState = eStageFrameworkState.SetUp;

        //Camera
        CameraManager.Instance.FadeOn(eCameraFadeType.BattleWipe, 1f, null);
        await UniTask.WaitForSeconds(1f);

        //���� ��ҵ� ��ȯ
        //ĳ���� - �� - ���� ���

        //Actor
        //var actor = await ActorManager.Instance.SpawnCharacter(1, Vector3.zero);
        //Player.RegisterPlayer(actor);

        //Bg
        // BackgroundManager.Instance.ShowBackgroundByStage(DataManager.StageTable[stageIndex].BackgroundPath);

        //UI
        //UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);

        Debug.Log("���� ���������� ���� ����� �غ� �Ϸ�");
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
                CurrentFrameworkState = eStageFrameworkState.Cancel;
            }
            finally
            {
                Debug.Log($"�����{CurrentFrameworkState}");
                //���â �����ֱ�
                //if (frameworkCTS.IsCancellationRequested == false)
                //    UIManager.Instance.ResultPopUpUI.Enable();
            }
        }
    }
    void StartFramework()
    {
        Debug.Log("������ ��ũ �۵� ����");
        CameraManager.Instance.FadeOff(false, eCameraFadeType.BattleWipe, 1f, null);
        //���͸� ���۽�Ű�� ����?
    }
    protected abstract UniTask ProcessFrameworkAsync(CancellationToken token);
    #endregion

    #region Stage Stop Method
    //���μ����� ���߰� ���â�� �����ش� .
    public void StopFramework(bool skipResult)
    {
        if (frameworkState == eStageFrameworkState.InProgress)
            frameworkCTS.Cancel();

        StopFrameworkAsync(skipResult).Forget();
    }
    //
    async UniTask StopFrameworkAsync(bool skipResult)
    {
        await UniTask.WaitUntil(() => frameworkState != eStageFrameworkState.InProgress);
        OnStopFramework();

        if (skipResult)
            CleanFramework();
       // else
           // UIManager.Instance.ResultPopUpUI.Enable();
    }
    //���������� �ʱ�ȭ�� �����Ѵ�
    //Remove Dynamic Things
    protected virtual void OnStopFramework()
    {
        CurrentFrameworkState = eStageFrameworkState.Defeat;
        //Player
        //Player.UnRegisterPlayer();
        //ActorManager.Instance.Clear();
    }
    #endregion

    #region Stage Clean Method
    //�̰� ��ҵ��� ��� �ʱ�ȭ �ϰ� ���� ���������� ���ư��� ����
    public void CleanFramework()
    {
        CurrentFrameworkState = eStageFrameworkState.Clean;
        OnCleanFramework();
        //�������� ������ ���� ó��
    }
    //Remove Static Things
    protected virtual void OnCleanFramework()
    {
        //BG
        //BackgroundManager.Instance.HideBackground();
        //UI
        UIManager.Instance.GameUI.CloseUIByFlag(eUI.Controller | eUI.BattleState);
        ExitStage(1f).Forget();
    }
    async UniTask ExitStage(float time)
    {
        await UniTask.WaitForSeconds(time);
        //���� - ���� ������ ���� �ٸ� �ε��� ���
        StageManager.Instance.SetupStage(eContentsType.Normal,Data.FailedIndex);
        //SceneManager.Instance.AsyncSceneChange<LobbyScene>().Forget();
    }

    #endregion
}
