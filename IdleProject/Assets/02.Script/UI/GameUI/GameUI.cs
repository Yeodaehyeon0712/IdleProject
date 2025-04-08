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
    public PlayerInfoUI PlayerInfo;
    public StageUI Stage;

    //PopUp UI
    public ResultPopUpUI ResultPopUp;
    #endregion

    public void Initialize()
    {
        Transform safeArea = transform.Find("SafeArea");
        InitializeSafeArea(safeArea);

        //Battle Scene
        var groupBattleSceneUI = safeArea.Find("Group_BattleSceneUI");
        PlayerInfo= groupBattleSceneUI.Find("Panel_Top/PlayerInfoUI").GetComponent<PlayerInfoUI>();
        uiDic.Add(eUI.PlayerInfo,PlayerInfo.Initialize());
        Stage= groupBattleSceneUI.Find("Panel_Top/StageUI").GetComponent<StageUI>();
        uiDic.Add(eUI.Stage,Stage.Initialize());
        Main = groupBattleSceneUI.Find("Panel_Middle/MainUI").GetComponent<MainUI>();
        uiDic.Add(eUI.Main, Main.Initialize());
        MenuButton = groupBattleSceneUI.Find("Panel_Bottom/MenuButtonUI").GetComponent<MenuButtonUI>();
        uiDic.Add(eUI.MenuButton, MenuButton.Initialize());

        //PopUp
        var groupPopUpUI = safeArea.Find("Group_PopUpUI");
        ResultPopUp = groupPopUpUI.Find("ResultPopUpUI").GetComponent<ResultPopUpUI>();
        uiDic.Add(eUI.ResultPopUp, ResultPopUp.Initialize());
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

