using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class LoopStageUI : BaseUI
{
    Button btn_Boss;
    TextMeshProUGUI text_Title;
    protected override void InitReference()
    {
        btn_Boss = transform.Find("Btn_Boss").GetComponent<Button>();
        btn_Boss.onClick.AddListener(()=>StageManager.Instance.StopStage(true));
        text_Title = transform.Find("Image_Title/Text_Title").GetComponent<TextMeshProUGUI>();
    }
    public void SetTitleText(string st)
    {
        text_Title.text = st;
    }
    protected override void OnRefresh()
    {

    }
}
