using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class CharacterData
    {
        public readonly int Index;
        public readonly int NameKey;
        public readonly string ResourcePath;
        public readonly string AnimatorPath;
        public readonly string IconPath;

        public CharacterData(int index,Dictionary<string, string> dataPair)
        {
            Index=index;
            NameKey = int.Parse(dataPair["NameKey"]);
            ResourcePath = dataPair["ResourcePath"];
            AnimatorPath = dataPair["AnimatorPath"];
            IconPath = dataPair["IconPath"];
        }
    }
}
public class CharacterTable : TableBase
{
    Dictionary<int, Data.CharacterData> characterDataDic = new Dictionary<int, Data.CharacterData>();
    public Dictionary<int, Data.CharacterData> GetDataDic => characterDataDic;
    public Data.CharacterData this[int index]
    {
        get
        {
            if (characterDataDic.ContainsKey(index))
                return characterDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            characterDataDic.Add(contents.Key, new Data.CharacterData(contents.Key, contents.Value));
    }
}
