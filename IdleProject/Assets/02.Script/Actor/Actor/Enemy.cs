using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    #region Variables
    public Data.EnemyData enemyData;
    #endregion

    protected override void InitializeComponent()
    {
        enemyData = DataManager.EnemyTable[objectID];
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(enemyData.AnimatorPath));
        statusComponent = new EnemyStatusComponent(this);
        fsmComponent = new EnemyFSMComponent(this);
    }
    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        DropGold();
    }
    void DropGold()
    {
        var snapShot = SnapShotDataProperty.Instance;
        var gold = DataManager.StageTable[snapShot.GetCurrentStage()].GetGold();
        snapShot.GetData.GoldAmount += gold;
    }
}
