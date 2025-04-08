using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SnapShotData
{
    //능력치 작업 중이었음 . 
    public Dictionary<eStatusType, int> statusLevelDic=new Dictionary<eStatusType, int>();
    public int CurrentStageIndex;
    public int GoldAmount;
}
//Have to Change Save In Server 
public class SnapShotDataProperty : JsonSerializableData<SnapShotData, SnapShotDataProperty>
{
    public SnapShotData GetData => data;
    protected override void SetDefaultValue()
    {
        data = new SnapShotData();
        foreach(eStatusType a in System.Enum.GetValues(typeof(eStatusType)))
        {
            data.statusLevelDic[a] = 1;
        }
        data.CurrentStageIndex = 1;
    }
    public int GetStatusLevel(eStatusType type)
    {
        return data.statusLevelDic[type];
    }
    public void StatusLevelUp(eStatusType type, int goldCost)
    {
        data.GoldAmount -= goldCost;
        data.statusLevelDic[type]+=1;
    }
    public int GetCurrentStage()
    {
        return data.CurrentStageIndex;
    }
}
