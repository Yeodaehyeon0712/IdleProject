using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalSalmon.Fade;


public class CameraManager : TSingletonMono<CameraManager>
{
    #region Fields
    Dictionary<eCameraType, BaseCamera> cameraDic = new Dictionary<eCameraType, BaseCamera>();
    public FadeEffect GetFadeEffect(eCameraFadeType type) => fadeEffectDic[type] ?? null;
    Dictionary<eCameraFadeType, FadeEffect> fadeEffectDic = new Dictionary<eCameraFadeType, FadeEffect>();
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
        CreateCamera(eCameraType.MainCamera);
        CreateCamera(eCameraType.UICamera);
        CreateFadeEffectDic();
        IsLoad = true;
    }
    #endregion

    #region CameraMethod
    BaseCamera CreateCamera(eCameraType type)
    {
        var prefab = Resources.Load<BaseCamera>("Camera/" + type.ToString());
        BaseCamera camera = Instantiate(prefab, transform); 
        cameraDic.Add(type, camera.Initialize());
        return camera;
    }
    public BaseCamera GetCamera(eCameraType cameraType)
    {
        if (cameraDic.ContainsKey(cameraType)==false)
            CreateCamera(cameraType);

        return cameraDic[cameraType];
    }
    public T GetCamera<T>(eCameraType cameraType) where T : BaseCamera
    {
        if (cameraDic.ContainsKey(cameraType) == false)
            CreateCamera(cameraType);

        return cameraDic[cameraType] as T;
    }
    void CreateFadeEffectDic()
    {
        for (eCameraFadeType i = 0; i < eCameraFadeType.End; ++i)
            fadeEffectDic.Add(i, Instantiate(Resources.Load<FadeEffect>($"FadeEffect/{i.ToString()}")));
    }
    #endregion

    #region Fade Method
    public void FadeOn(bool useUI,eCameraFadeType type, float durationTime, System.Action action)
    {
        cameraDic[eCameraType.MainCamera].Fade(isOn:true,type, durationTime, action);
        if(useUI)
            cameraDic[eCameraType.UICamera].Fade(isOn: true, type, durationTime, null);
    }
    public void FadeOff(bool useUI, eCameraFadeType type, float durationTime, System.Action action)
    {
        cameraDic[eCameraType.MainCamera].Fade(isOn: false, type, durationTime, action);
        if (useUI)
            cameraDic[eCameraType.UICamera].Fade(isOn: false, type, durationTime, null);
    }
    #endregion
}
