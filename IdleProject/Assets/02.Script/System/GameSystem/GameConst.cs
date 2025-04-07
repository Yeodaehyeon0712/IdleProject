using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    #region Data
    public static Dictionary<long, string> LongToString;
    public static Dictionary<string, long> StringToLong;
    #endregion

    #region Camera
    public static Vector2 defaultResolution = new Vector2(1080, 1920);
    public static Vector3 ViewOffset = new Vector3(8f, 3f);
    #endregion

    #region Path
#if UNITY_EDITOR
    public static readonly string CacheDirectoryPath = "Cache/";
    public static readonly string LogDirectoryPath = CacheDirectoryPath + "/Log/";
#else
    public static readonly string CacheDirectoryPath = UnityEngine.Application.persistentDataPath + "/Cache/";
    public static readonly string LogDirectoryPath = CacheDirectoryPath + "/Log/";
#endif
    #endregion

    #region Enum Converter
    public static Dictionary<string, eStageType> StageType;

    public static void InitializeEnumConverter()
    {
        OnGenerateEnumContainer(ref StageType);
    }
    public static void ClearEnumConverter()
    {
        //StringToStatusType = null;  
    }
    static void OnGenerateEnumContainer<T>(ref Dictionary<string, T> container) where T : Enum
    {
        Type type = typeof(T);
        string[] names = Enum.GetNames(type);
        Array values = Enum.GetValues(type);
        container = new Dictionary<string, T>(names.Length);
        int count = 0;
        foreach (var element in values)
            container.Add(names[count++], (T)element);
    }
    #endregion

    public static void Initialize()
    {
        if (System.IO.Directory.Exists(CacheDirectoryPath) == false)
            System.IO.Directory.CreateDirectory(CacheDirectoryPath);

        StringToLong = new Dictionary<string, long>(200);
        LongToString = new Dictionary<long, string>(200);
        for (long i = 1; i < 201; ++i)
        {
            string key = i.ToString();
            StringToLong.Add(key, i);
            LongToString.Add(i, key);
        }
        InitializeEnumConverter();
        //for(eStatusType i = 0; i < eStatusType.End; ++i)
        //{
        //    Sprite sprite = Resources.Load<Sprite>("");
        //}
    }
}
