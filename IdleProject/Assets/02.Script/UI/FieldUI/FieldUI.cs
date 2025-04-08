using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFieldUIType
{
    HPBar,
    DamageText,
    SpeachBubble,
    BGEffect
}
public class FieldUI : MonoBehaviour
{
    Dictionary<EFieldUIType, MemoryPool<BaseFieldUI>> _memoryPoolDic = new Dictionary<EFieldUIType, MemoryPool<BaseFieldUI>>(2);
   

    public void SetDamageText(Vector3 pos, double damage, bool isCritical, eActorType type)
    {
        FindFieldUI<FieldUI_DamageText>(EFieldUIType.DamageText).Enabled(pos, damage, isCritical, type);
    }
    
    T FindFieldUI<T>(EFieldUIType type) where T : BaseFieldUI
    {
        T ui = _memoryPoolDic[type].GetItem() as T;
        if (ui != null)
            return ui;

        ui = Instantiate(Resources.Load<T>("UI/FieldUI/" + type.ToString()), transform);
        ui.Init(_memoryPoolDic[type].Register);
        return ui;
    }
    public void Initialize()
    {
        _memoryPoolDic.Add(EFieldUIType.DamageText, new MemoryPool<BaseFieldUI>(20));
        transform.SetParent(CameraManager.Instance.GetCamera(eCameraType.MainCamera).transform);
    }
}
