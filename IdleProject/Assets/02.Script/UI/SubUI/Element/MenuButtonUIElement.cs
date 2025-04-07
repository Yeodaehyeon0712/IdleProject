using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MenuButtonUIElement :BaseUI
{
    #region Fields
    RectTransform rect_btnRect;
    RectTransform rect_icon;
    SwitchButton btn_Select;

    Vector2 originalValue;
    Vector2 targetValue;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        rect_btnRect = GetComponent<RectTransform>();
        originalValue = rect_btnRect.sizeDelta;
        targetValue = new Vector2(rect_btnRect.sizeDelta.x+80, rect_btnRect.sizeDelta.y);
        btn_Select = GetComponent<SwitchButton>();
        btn_Select.SetImage(false);
        rect_icon = transform.Find("Image_Icon").GetComponent<RectTransform>();
    }

    protected override void OnRefresh()
    {
        
    }
    public MenuButtonUIElement InitElement(UnityAction action)
    {
        Initialize();
        Enable();
        btn_Select.onClick.AddListener(action);
        return this;
    }
    #endregion

    #region Focus Method
    public void FocusOn()
    {
        btn_Select.SetImage(true);
        rect_btnRect.DOSizeDelta(targetValue, 0.25f);
        rect_icon.DOScale(2, 0.25f);
    }
    public void FocusOut()
    {
        btn_Select.SetImage(false);
        rect_btnRect.DOSizeDelta(originalValue, 0.25f);
        rect_icon.DOScale(1, 0.25f);
    }
    #endregion
}
