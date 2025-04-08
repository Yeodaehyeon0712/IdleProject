using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPopUpUI : PopUpUI
{
    #region Variables
    TextMeshProUGUI text_Result;
    Button btn_Confirm;
    TextMeshProUGUI text_ConfirmBtnTitle;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        var panel_Result = transform.Find("Panel_Result");
        text_Result = panel_Result.Find("Image_Title/Text_Description").GetComponent<TextMeshProUGUI>();
        var panel_Bottom= panel_Result.Find("Panel_Bottom");
        btn_Confirm = panel_Bottom.Find("Btn_Confirm").GetComponent<Button>();
        btn_Confirm.onClick.AddListener(Confirm);
        text_ConfirmBtnTitle = btn_Confirm.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        text_ConfirmBtnTitle.text = LocalizingManager.Instance.GetLocalizing(1);
    }
    #endregion

    public override void Enable()
    {
        base.Enable();
        SetResult();
    }

    void SetResult()
    {
        var result = StageManager.Instance.GetCurrentFrameworkState();
        text_Result.text = result.ToString();
    }
    public void Confirm()
    {      
        Disable();
        StageManager.Instance.ClearStage();
    }
}
