using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusElementUI : BaseUI
{
    #region Variables
    eStatusType type;
    Image image_Icon;
    TextMeshProUGUI text_Level;
    TextMeshProUGUI text_Description;
    TextMeshProUGUI text_EnforceTitle;
    TextMeshProUGUI text_GoldAmount;
    Button btn_Enforce;

    StatusMainUI statusUI;
    Data.StatusData data;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        image_Icon= transform.Find("Image_Icon").GetComponent<Image>();
        text_Level = transform.Find("Text_Level").GetComponent<TextMeshProUGUI>();
        text_Description= transform.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();
        text_EnforceTitle = transform.Find("Button_Enforce/Text_Enforce").GetComponent<TextMeshProUGUI>();
        text_GoldAmount = transform.Find("Button_Enforce/Image_GoldAmount/Text_GoldAmount").GetComponent<TextMeshProUGUI>();
        btn_Enforce = transform.Find("Button_Enforce").GetComponent<Button>();
        btn_Enforce.onClick.AddListener(OnClickEnforce);
    }
    public void SetStatusType(StatusMainUI statusUI,eStatusType type)
    {
        this.statusUI = statusUI;
        this.type = type;
        data = DataManager.StatusTable[type];
        image_Icon.sprite = AddressableSystem.GetIcon(data.IconPath);
        SetStatusElement();
        Enable();
    }
    protected override void OnRefresh()
    {
        text_EnforceTitle.text=LocalizingManager.Instance.GetLocalizing(1);
    }
    #endregion

    public void SetStatusElement()
    {
        var snapShot = SnapShotDataProperty.Instance;
        int currentLevel = snapShot.GetStatusLevel(type);
        int goldCost = data.GetGold(currentLevel);
        bool enableEnforce = snapShot.GetData.GoldAmount >= goldCost;

        text_Level.text = $"Lv. {currentLevel}";
        text_GoldAmount.text = goldCost.ToString();
        btn_Enforce.interactable= enableEnforce;
    }
    void OnClickEnforce()
    {
        var snapShot = SnapShotDataProperty.Instance;
        var currentLevel = snapShot.GetStatusLevel(type);

        int goldCost = data.GetGold(currentLevel);
        bool enableEnforce = snapShot.GetData.GoldAmount >= goldCost;

        if (enableEnforce == false) return;

        snapShot.StatusLevelUp(type,data.GetGold(currentLevel));
        statusUI.RefreshElement();
    }
}
