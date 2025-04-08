using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusComponent : StatusComponent
{
    #region Variables
    Dictionary<eStatusType, float> computedStatusDic = new Dictionary<eStatusType, float>();
    #endregion

    #region Component Method
    public CharacterStatusComponent(Actor owner) : base(owner)
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
        var statusData = DataManager.StatusTable[type];

        float defaultValue = statusData.DefaultValue;
        float enforceValue = statusData.GetValue(SnapShotDataProperty.Instance.GetStatusLevel(type));;

        computedStatusDic[type] = statusData.CalculateType switch
        {
            eCalculateType.Flat => (defaultValue + enforceValue),
            eCalculateType.Percentage => defaultValue * (1f + (enforceValue * 0.01f)),
            _ => defaultValue,
        };
    }
    #endregion
}
