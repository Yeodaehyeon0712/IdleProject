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
    //각종 스테이지에 대한 공통된 준비
    public virtual async UniTask SetupFrameworkAsync(int stageIndex)
    {
        Debug.Log("각종 스테이지에 대한 공통된 준비 시작");
        this.stageIndex = stageIndex;
        CurrentFrameworkState = eStageFrameworkState.SetUp;

        //Camera
        CameraManager.Instance.FadeOn(eCameraFadeType.BattleWipe, 1f, null);
        await UniTask.WaitForSeconds(1f);

        //각종 요소들 소환
        //캐릭터 - 맵 - 사운드 등등

        //Actor
        //var actor = await ActorManager.Instance.SpawnCharacter(1, Vector3.zero);
        //Player.RegisterPlayer(actor);

        //Bg
        // BackgroundManager.Instance.ShowBackgroundByStage(DataManager.StageTable[stageIndex].BackgroundPath);

        //UI
        //UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);

        Debug.Log("각종 스테이지에 대한 공통된 준비 완료");
    }

    //각종 스테이지를 개시하는 단계
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
                Debug.Log($"결과는{CurrentFrameworkState}");
                //결과창 보여주기
                //if (frameworkCTS.IsCancellationRequested == false)
                //    UIManager.Instance.ResultPopUpUI.Enable();
            }
        }
    }
    void StartFramework()
    {
        Debug.Log("프레임 워크 작동 시작");
        CameraManager.Instance.FadeOff(false, eCameraFadeType.BattleWipe, 1f, null);
        //액터를 동작시키는 정도?
    }
    protected abstract UniTask ProcessFrameworkAsync(CancellationToken token);
    #endregion

    #region Stage Stop Method
    //프로세스를 멈추고 결과창을 보여준다 .
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
    //스테이지의 초기화를 진행한다
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
    //이건 요소들을 모두 초기화 하고 다음 스테이지로 나아가기 위함
    public void CleanFramework()
    {
        CurrentFrameworkState = eStageFrameworkState.Clean;
        OnCleanFramework();
        //스테이지 나가기 까지 처리
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
        //성공 - 실패 유무에 따라 다른 인덱스 사용
        StageManager.Instance.SetupStage(eContentsType.Normal,Data.FailedIndex);
        //SceneManager.Instance.AsyncSceneChange<LobbyScene>().Forget();
    }

    #endregion
}
