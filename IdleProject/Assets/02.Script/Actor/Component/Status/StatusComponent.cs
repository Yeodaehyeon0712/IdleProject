using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class StatusComponent : BaseComponent
{
    #region Component Method
    public StatusComponent(Actor owner):base(owner,eComponent.StatusComponent,useUpdate:false)
    {

    }
    protected override void OnComponentActive()
    {
        SetDefaultStatus();
    }
    #endregion
    #region Status Method
    public abstract float GetStatus(eStatusType type);
    public abstract void SetDefaultStatus();
    public abstract void RecomputeStatus(eStatusType type);
    #endregion
}
