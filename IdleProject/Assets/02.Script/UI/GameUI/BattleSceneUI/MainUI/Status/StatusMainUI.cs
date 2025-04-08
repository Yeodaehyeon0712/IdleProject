using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMainUI : BaseUI
{
    List<StatusElementUI> elementsList=new List<StatusElementUI>();
    Transform elementParent;
    protected override void InitReference()
    {
        elementParent = transform.Find("Scroll View/Viewport/Content");
        var prefab = Resources.Load<StatusElementUI>("UI/Element/StatusElementUI");
        foreach(eStatusType status in System.Enum.GetValues(typeof(eStatusType)))
        {
            var element=Instantiate(prefab, elementParent);
            element.Initialize();
            element.SetStatusType(status);
            elementsList.Add(element);
        }
    }

    protected override void OnRefresh()
    {
        
    }

}
