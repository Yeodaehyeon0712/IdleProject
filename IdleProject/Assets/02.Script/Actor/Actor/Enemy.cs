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
    }
}
