using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : BaseUI
{
    #region Fields
    Dictionary<eMainUI, BaseUI> mainSubUI = new Dictionary<eMainUI, BaseUI>();
    eMainUI currentMainUI;

    public StatusMainUI StatusUI=>mainSubUI[eMainUI.Status] as StatusMainUI;
    //ItemMainUI ItemUI;
    //SkillMainUI SkillUI;
    //PartnerMainUI PartnerUI;
    //StoreMainUI StoreUI;

    public eMainUI CurrentLobby
    {
        get => currentMainUI;
        set
        {
            mainSubUI[currentMainUI].Disable();
            currentMainUI = value;
            mainSubUI[currentMainUI].Enable();
        }
    }
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        var statusUI = transform.Find("StatusMainUI").GetComponent<StatusMainUI>();
        mainSubUI.Add(eMainUI.Status, statusUI.Initialize());
        var itemUI = transform.Find("ItemMainUI").GetComponent<ItemMainUI>();
        mainSubUI.Add(eMainUI.Item, itemUI.Initialize());
        var skillUI = transform.Find("SkillMainUI").GetComponent<SkillMainUI>();
        mainSubUI.Add(eMainUI.Skill, skillUI.Initialize());
        var partnerUI = transform.Find("PartnerMainUI").GetComponent<PartnerMainUI>();
        mainSubUI.Add(eMainUI.Partner, partnerUI.Initialize());
        var storeUI = transform.Find("StoreMainUI").GetComponent<StoreMainUI>();
        mainSubUI.Add(eMainUI.Store, storeUI.Initialize());
    }
    protected override void OnRefresh()
    {

    }

    #endregion
}
