using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    #region Stage
    public static int RaceStageCount = 5;
    public static int BackgroundOffsetX = 9;
    public static int MaxBackgroundCount = 3;
    #endregion

    #region Data
    public static Dictionary<long, string> LongToString;
    public static Dictionary<string, long> StringToLong;

    #endregion

    #region Camera
    public static float DefaultOrthoSize = 5;
    public static Vector2 defaultResolution = new Vector2(1080, 1920);
    public static Vector3 ViewOffset = new Vector3(0f, -2f,-10f);
    public static float YOffSet = -2f;
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
    public static Dictionary<string, eStatusType> StatusType;
    public static Dictionary<string, eCalculateType> CalculateType;


    public static void InitializeEnumConverter()
    {
        OnGenerateEnumContainer(ref StageType);
        OnGenerateEnumContainer(ref StatusType);
        OnGenerateEnumContainer(ref CalculateType);
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
