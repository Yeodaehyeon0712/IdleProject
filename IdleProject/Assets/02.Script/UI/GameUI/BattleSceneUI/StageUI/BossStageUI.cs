using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossStageUI : BaseUI
{
    TextMeshProUGUI text_Title;
    protected override void InitReference()
    {
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
