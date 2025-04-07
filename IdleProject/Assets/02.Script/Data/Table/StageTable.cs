using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class StageData
    {  
        public readonly int Index;
        public readonly eStageType Type;
        public readonly int SuccessIndex;
        public readonly int FailedIndex;
        public readonly int[] MonsterIndexArr;
        //HP
        //Attack
        //Gold
        public readonly string BackgroundKey; 

        public StageData(int index, Dictionary<string, string> dataPair)
        {
            Index = index;
            Type = GameConst.StageType[dataPair["StageType"]];
            SuccessIndex = int.Parse(dataPair["SuccessIndex"]);
            FailedIndex = int.Parse(dataPair["FailedIndex"]);
            MonsterIndexArr = System.Array.ConvertAll(dataPair["MonsterIndex"].Split('|'), v => int.Parse(v));
            //HP
            //Attack
            //Gold
            BackgroundKey = dataPair["BackgroundKey"];
        }
    }

}
public class StageTable : TableBase
{
    Dictionary<int, Data.StageData> stageDataDic = new Dictionary<int, Data.StageData>();
    public Dictionary<int, Data.StageData> GetDataDic => stageDataDic;
    public Data.StageData this[int index]
    {
        get
        {
            if (stageDataDic.ContainsKey(index))
                return stageDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            stageDataDic.Add(contents.Key, new Data.StageData(contents.Key, contents.Value));
    }
}
