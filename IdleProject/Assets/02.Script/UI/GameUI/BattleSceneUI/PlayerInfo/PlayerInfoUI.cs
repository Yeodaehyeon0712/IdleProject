using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : BaseUI
{
    #region Fields
    Button btn_Setting;
    Image image_PlayerAvatar;
    Slider slider_HP;
    //Goods
    TextMeshProUGUI text_GoldCount;
    TextMeshProUGUI text_GemCount;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        btn_Setting = transform.Find("Btn_Setting").GetComponent<Button>();
        //btn_Setting.onClick.AddListener(() => UIManager.Instance.SettingPopUpUI.Enable());
        var panel_PlayerInfo = transform.Find("Panel_PlayerInfo");
        image_PlayerAvatar = panel_PlayerInfo.Find("Image_PlayerAvatar").GetComponent<Image>();
        slider_HP = panel_PlayerInfo.Find("Slider_HP").GetComponent<Slider>();
        var panel_Goods = panel_PlayerInfo.Find("Panel_Goods");
        text_GoldCount = panel_Goods.Find("Panel_Gold/Text_Count").GetComponent<TextMeshProUGUI>();
        text_GemCount = panel_Goods.Find("Panel_Gem/Text_Count").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        
    }
    #endregion
    public void SetGoldCount(int count)
    {
        text_GoldCount.text = count.ToString();
    }
    public void SetHPPercentage(float percentage)
    {
        slider_HP.value = percentage;
    }
    public void SetGemCount(int count)
    {
        text_GemCount.text = count.ToString();
    }
}
