using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonUI :BaseUI
{
    #region Fields
    Dictionary<eMainUI, MenuButtonUIElement> buttonDic=new Dictionary<eMainUI, MenuButtonUIElement>();
    eMainUI currentMenu=eMainUI.Store;

    public eMainUI CurrentMenu
    {
        get => currentMenu;
        set
        {
            if (value == currentMenu)
                return;

            buttonDic[currentMenu].FocusOut();
            currentMenu = value;
            buttonDic[currentMenu].FocusOn();
            UIManager.Instance.MainUI.CurrentLobby = currentMenu;
        }
    }
    #endregion

    #region Init Method
    public override void Enable()
    {
        base.Enable();
        CurrentMenu = eMainUI.Status;
    }
    protected override void InitReference()
    {
        var panel_btn = transform.Find("Panel_Button");

        var btn_Status = panel_btn.Find("Btn_Status").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eMainUI.Status, btn_Status.InitElement(() => CurrentMenu = eMainUI.Status));

        var btn_Item = panel_btn.Find("Btn_Item").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eMainUI.Item, btn_Item.InitElement(() => CurrentMenu = eMainUI.Item));

        var btn_Skill = panel_btn.Find("Btn_Skill").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eMainUI.Skill, btn_Skill.InitElement(() => CurrentMenu = eMainUI.Skill));

        var btn_Partner = panel_btn.Find("Btn_Partner").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eMainUI.Partner, btn_Partner.InitElement(() => CurrentMenu = eMainUI.Partner));

        var btn_Store = panel_btn.Find("Btn_Store").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eMainUI.Store, btn_Store.InitElement(() => CurrentMenu = eMainUI.Store));
    }
    protected override void OnRefresh()
    {
        
    }
    #endregion
}
