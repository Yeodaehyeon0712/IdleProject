using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : TSingletonMono<UIManager>
{
    #region Fields
    public GameUI GameUI => _gameUI;
    GameUI _gameUI;

    //Battle Scene UI
    public MenuButtonUI MenuButtonUI => _gameUI.MenuButton;
    #endregion

    protected override void OnInitialize()
    {
        _gameUI = Instantiate(Resources.Load<GameUI>("UI/GameUI"), transform);
        _gameUI.Initialize();     
        InitCanvas(_gameUI);
        IsLoad = true;
    }
    void InitCanvas(Component root,Camera targetCamera=null)
    {
        Canvas canvas = root.GetComponent<Canvas>();
        CanvasScaler scaler = root.GetComponent<CanvasScaler>();        

        if(targetCamera!=null)
        {
            canvas.worldCamera = targetCamera;
            canvas.planeDistance = 1;
        }

        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080,1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1F;
        }
    }
}
