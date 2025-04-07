using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    protected override void OnStartScene()
    {
        StageManager.Instance.SetupStage(eContentsType.Normal, 1);
    }

    protected override void OnStopScene()
    {

    }
}
