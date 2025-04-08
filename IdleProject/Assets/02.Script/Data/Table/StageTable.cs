using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class StageData
    {  
        public readonly int Index;
        public readonly string Name;
        public readonly eStageType Type;
        public readonly int SuccessIndex;
        public readonly int FailedIndex;
        public readonly int[] EnemyIndexArr;
        public readonly int EnemyCount;
        readonly FloatFormulaCalculator enemyHPFormula;
        readonly FloatFormulaCalculator enemyAttackDamageFormula;
        readonly IntFormulaCalculator enemyGoldFormula;
        public readonly string BackgroundKey; 

        public StageData(int index, Dictionary<string, string> dataPair)
        {
            Index = index;
            Name = dataPair["Name"];
            Type = GameConst.StageType[dataPair["StageType"]];
            SuccessIndex = int.Parse(dataPair["SuccessIndex"]);
            FailedIndex = int.Parse(dataPair["FailedIndex"]);
            EnemyIndexArr = System.Array.ConvertAll(dataPair["EnemyIndex"].Split('|'), v => int.Parse(v));
            EnemyCount = int.Parse(dataPair["EnemyCount"]);
            enemyHPFormula = new FloatFormulaCalculator(dataPair["EnemyMaxHP"]);
            enemyAttackDamageFormula = new FloatFormulaCalculator(dataPair["EnemyAttackDamage"]);
            enemyGoldFormula = new IntFormulaCalculator(dataPair["EnemyGoldAmount"]);
            BackgroundKey = dataPair["BackgroundKey"];
        }
        public float GetEnemyMaxHP() => enemyHPFormula.GetValue(Index);
        public float GetEnemyAttackDamage() => enemyAttackDamageFormula.GetValue(Index);
        public int GetGold() => enemyGoldFormula.GetValue(Index);
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
