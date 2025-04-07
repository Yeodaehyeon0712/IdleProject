using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : BaseUI
{
    #region Fields
    Dictionary<eStageType, BaseUI> stageUIDic = new Dictionary<eStageType,BaseUI>();
    eStageType currentStage;

    public eStageType CurrentStage
    {
        get => currentStage;
        set
        {
            if (value == currentStage)
                return;

            stageUIDic[currentStage].Enable();
            currentStage = value;
            stageUIDic[currentStage].Disable();
        }
    }
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        var raceStageUI = transform.Find("RaceStageUI").GetComponent<RaceStageUI>();
        stageUIDic.Add(eStageType.Race,raceStageUI.Initialize());
        var loopStageUI= transform.Find("LoopStageUI").GetComponent<LoopStageUI>();
        stageUIDic.Add(eStageType.Loop, loopStageUI.Initialize());
        var bossStageUI= transform.Find("BossStageUI").GetComponent<BossStageUI>();
        stageUIDic.Add(eStageType.Boss, bossStageUI.Initialize());
    }
    protected override void OnRefresh()
    {

    }
    #endregion
}
