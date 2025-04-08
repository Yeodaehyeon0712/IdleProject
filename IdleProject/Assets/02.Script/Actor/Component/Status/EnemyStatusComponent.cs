using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStatusComponent : StatusComponent
{
    #region Variables
    Dictionary<eStatusType, float> computedStatusDic = new Dictionary<eStatusType, float>();
    #endregion

    # region Component Method
    public EnemyStatusComponent(Actor owner) : base(owner)
    {

    }
    #endregion

    #region Status Method
    public override float GetStatus(eStatusType type) => computedStatusDic[type];
    public override void SetDefaultStatus()
    {
        foreach (eStatusType type in System.Enum.GetValues(typeof(eStatusType)))
        {
            RecomputeStatus(type);
        }
    }
    public override void RecomputeStatus(eStatusType type)
    {
        var stageData=DataManager.StageTable[SnapShotDataProperty.Instance.GetCurrentStage()];

        computedStatusDic[type] = type switch
        {
            eStatusType.AttackDamage => stageData.GetEnemyAttackDamage(),
            eStatusType.MaxHP => stageData.GetEnemyMaxHP(),
            _ => 0,
        };
    }
    #endregion
}
