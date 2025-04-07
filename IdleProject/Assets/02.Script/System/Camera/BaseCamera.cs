using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalSalmon.Fade;


public class BaseCamera : MonoBehaviour
{
    #region Fields
    public Camera Camera => _camera;
    Camera _camera;
    public FadePostProcess FadeProcess => fadeEffectProcess;
    protected FadePostProcess fadeEffectProcess;
    public eCameraFadeType CurrentFadeType => currentType;
    eCameraFadeType currentType;
    public bool IsFading => fadeEffectProcess.IsFading;
    public bool Enabled
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }
    #endregion

    #region Init Method
    public virtual BaseCamera Initialize()
    {
        _camera = GetComponentInChildren<Camera>();
        fadeEffectProcess = GetComponent<FadePostProcess>();

        currentType = 0;
        transform.position = GameConst.ViewOffset;

        float targetRate = GameConst.defaultResolution.x / GameConst.defaultResolution.y;
        float screenRate = Screen.safeArea.width / Screen.safeArea.height;

        Camera.orthographicSize = 6.4f * screenRate / targetRate;
        return this;
    }
    #endregion

    #region Fade Method
    public void Fade(bool isOn, eCameraFadeType type, float durationTime, System.Action action)
    {
        currentType = type;
        fadeEffectProcess.AssignEffect(CameraManager.Instance.GetFadeEffect(type), true);
        fadeEffectProcess.effectDuration = durationTime;

        if(isOn)
            fadeEffectProcess.FadeDown(false, action);
        else
            fadeEffectProcess.FadeUp(false, action);
    }
    #endregion
}
