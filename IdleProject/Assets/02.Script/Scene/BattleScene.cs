using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    protected override void OnStartScene()
    {
        CameraManager.Instance.FadeOn(true, eCameraFadeType.Blur, 0.1f, null);
        UIManager.Instance.OpenUIByFlag(eUI.MenuButton | eUI.Main | eUI.PlayerInfo|eUI.Stage);
        StageManager.Instance.SetupStage(eContentsType.Normal, 1);
    }

    protected override void OnStopScene()
    {

    }
}
