using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    Dictionary<eContentsType, StageFramework> stageFrameworkDic;
    [SerializeField] eContentsType prevContents;
    [SerializeField] eContentsType currContents;
    bool isChanging;
    public bool LockStage;
    CancellationTokenSource mainProcessCts;
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
        stageFrameworkDic = new Dictionary<eContentsType, StageFramework>((int)eContentsType.End)
        {
            { eContentsType.Normal,new NormalStageFramework()},
        };
        IsLoad = true;
    }
    #endregion

    #region Stage Method
    //Setup Method
    public void SetupStage(eContentsType type, int stageIndex)
    {
        if (isChanging) return;
        isChanging = true;

        if (mainProcessCts != null)
            mainProcessCts.Cancel();
        mainProcessCts = new CancellationTokenSource();

        prevContents = currContents;
        currContents = type;

        SetupStageAsync(currContents, stageIndex,mainProcessCts).Forget();
    }
    async UniTask SetupStageAsync(eContentsType type, int stageIndex, CancellationTokenSource token)
    {
        try
        {
            await stageFrameworkDic[currContents].SetupFrameworkAsync(stageIndex);

            while (LockStage)
                await UniTask.Yield(cancellationToken:token.Token);

            stageFrameworkDic[currContents].StartFrameworkAsync(stageIndex).Forget();
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("SetupStageAsync was canceled.");
        }
        finally
        {
            token.Dispose();
            mainProcessCts = null;
            isChanging = false;
        }
    }
    //Stop Method
    public void StopStage(bool skipResult)
    {
        stageFrameworkDic[currContents].StopFramework(skipResult);
    }
    public void ClearStage()
    {
        stageFrameworkDic[currContents].CleanFramework();
    }
    #endregion

    #region Framework Method
    public T GetFramework<T>(eContentsType type)where T:StageFramework
    {
        return stageFrameworkDic[type] as T;
    }
    public eStageFrameworkState GetCurrentFrameworkState()
    {
        return stageFrameworkDic[currContents].CurrentFrameworkState;
    }
    #endregion
}
