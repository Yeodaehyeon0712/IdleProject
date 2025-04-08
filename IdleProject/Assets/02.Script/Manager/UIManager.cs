using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : TSingletonMono<UIManager>
{
    #region Fields
    public GameUI GameUI => _gameUI;
    GameUI _gameUI;
    public FieldUI FieldUI => _fieldUI;
    FieldUI _fieldUI;

    //Battle Scene UI
    public MainUI MainUI => _gameUI.Main;
    public MenuButtonUI MenuButtonUI => _gameUI.MenuButton;
    public PlayerInfoUI PlayerInfoUI=>_gameUI.PlayerInfo;
    public StageUI Stage=>_gameUI.Stage;

    //Pop Up UI
    public ResultPopUpUI ResultPopUpUI => _gameUI.ResultPopUp;
    #endregion

    protected override void OnInitialize()
    {
        _fieldUI = Instantiate(Resources.Load<FieldUI>("UI/FieldUI"), transform);
        _fieldUI.Initialize();
        InitCanvas(_fieldUI, CameraManager.Instance.GetCamera(eCameraType.MainCamera).Camera);

        _gameUI = Instantiate(Resources.Load<GameUI>("UI/GameUI"), transform);
        _gameUI.Initialize();     
        InitCanvas(_gameUI, CameraManager.Instance.GetCamera(eCameraType.UICamera).Camera);
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
            scaler.referenceResolution = GameConst.defaultResolution;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1F;
        }
    }
    public void OpenUIByFlag(eUI flaggedEnum)
    {
        _gameUI.OpenUIByFlag(flaggedEnum);
    }
    public void CloseUIByFlag(eUI flaggedEnum)
    {
        _gameUI.CloseUIByFlag(flaggedEnum);
    }
}
