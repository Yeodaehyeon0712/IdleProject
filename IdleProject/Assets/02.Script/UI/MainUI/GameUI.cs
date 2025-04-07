using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    #region Fields
    Dictionary<eUI, BaseUI> uiDic = new Dictionary<eUI,BaseUI>();

    //Battle Scene UI
    public MainUI Main;
    public MenuButtonUI MenuButton;
    
    #endregion

    public void Initialize()
    {
        Transform safeArea = transform.Find("SafeArea");
        InitializeSafeArea(safeArea);

        //Lobby Scene
        var groupBattleSceneUI = safeArea.Find("Group_BattleSceneUI");
        MenuButton = groupBattleSceneUI.Find("MenuButtonUI").GetComponent<MenuButtonUI>();
        uiDic.Add(eUI.MenuButton, MenuButton.Initialize());
        Main = groupBattleSceneUI.Find("MainUI").GetComponent<MainUI>();
        uiDic.Add(eUI.Main, Main.Initialize());
    }
    void InitializeSafeArea(Transform safeArea)
    {
        SafeArea safeAreaUI = safeArea.GetComponent<SafeArea>();

        LetterBox letterBox_Top= transform.Find("LetterBox_Top").GetComponent<LetterBox>();
        letterBox_Top.Initialize(safeAreaUI);

        LetterBox letterBox_Bottom = transform.Find("LetterBox_Bottom").GetComponent<LetterBox>();
        letterBox_Bottom.Initialize(safeAreaUI);

        safeAreaUI.Initialize();
    }
    public void OpenUIByFlag(eUI flaggedEnum)
    {       
        foreach (var uiEnum in uiDic.Keys)
        {
            if ((uiEnum & flaggedEnum) == 0) continue;

            uiDic[uiEnum].Enable();
        }
    }
    public void CloseUIByFlag(eUI flaggedEnum)
    {
        foreach (var uiEnum in uiDic.Keys)
        {
            if ((uiEnum & flaggedEnum) == 0) continue;

            uiDic[uiEnum].Disable();
        }
    }
}

