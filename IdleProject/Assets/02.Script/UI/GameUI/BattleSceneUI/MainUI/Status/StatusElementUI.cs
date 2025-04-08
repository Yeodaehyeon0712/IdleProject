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
    }
    public void SetStatusType(eStatusType type)
    {
        this.type = type;
        data = DataManager.StatusTable[type];
        image_Icon.sprite = AddressableSystem.GetIcon(data.IconPath);
        SetStatusData();
    }
    protected override void OnRefresh()
    {
        text_EnforceTitle.text=LocalizingManager.Instance.GetLocalizing(1);
    }
    #endregion

    void SetStatusData()
    {
        var currentLevel = SnapShotDataProperty.Instance.GetStatusLevel(type);
        text_Level.text = $"Lv. {currentLevel.ToString()}";
        text_GoldAmount.text = data.GetGold(currentLevel).ToString();
    }

}
