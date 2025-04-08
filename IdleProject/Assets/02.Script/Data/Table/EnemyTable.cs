using System.Collections.Generic;
namespace Data
{
    public class EnemyData
    {
        public readonly int Index;
        public readonly int NameKey;
        public readonly string ResourcePath;
        public readonly string AnimatorPath;
        public readonly string IconPath;

        public EnemyData(int index, Dictionary<string, string> dataPair)
        {
            Index = index;
            NameKey = int.Parse(dataPair["NameKey"]);
            ResourcePath = dataPair["ResourcePath"];
            AnimatorPath = dataPair["AnimatorPath"];
            IconPath = dataPair["IconPath"];
        }
    }
}
public class EnemyTable : TableBase
{
    Dictionary<int, Data.EnemyData> enemyDataDic = new Dictionary<int, Data.EnemyData>();
    public Dictionary<int, Data.EnemyData> GetDataDic => enemyDataDic;
    public Data.EnemyData this[int index]
    {
        get
        {
            if (enemyDataDic.ContainsKey(index))
                return enemyDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            enemyDataDic.Add(contents.Key, new Data.EnemyData(contents.Key, contents.Value));
    }
}
