using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Variables
    public Data.CharacterData characterData;
    #endregion
    protected override void InitializeComponent()
    {
        characterData = DataManager.CharacterTable[objectID];
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(characterData.AnimatorPath));
        statusComponent = new CharacterStatusComponent(this);
    }
    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        Timer.SetTimer(time, true, () => StageManager.Instance.StopStage(skipResult: false));
    }

}
