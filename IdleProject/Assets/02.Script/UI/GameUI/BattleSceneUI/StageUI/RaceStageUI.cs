using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class RaceStageUI :BaseUI
{
    TextMeshProUGUI text_Title;
    Image image_Progress;
    protected override void InitReference()
    {
        text_Title = transform.Find("Image_Title/Text_Title").GetComponent<TextMeshProUGUI>();
        image_Progress = transform.Find("Image_ProcessBG/Image_Process").GetComponent<Image>();
    }

    protected override void OnRefresh()
    {

    }
    public void SetTitleText(string st)
    {
        text_Title.text = st;
    }
    public void SetProgress(float value)
    {
        image_Progress.DOFillAmount(value, 0.3f).SetEase(Ease.OutQuad);
    }

}
