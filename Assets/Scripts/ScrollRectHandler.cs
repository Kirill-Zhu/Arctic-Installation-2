using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ScrollRectHandler : ScrollRect
{
   private bool _canDrag = true;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (_canDrag)
            base.OnBeginDrag(eventData);
        else
            Debug.LogWarning("Drag is Disabled");
    }

    public void CanDrag(bool canDrag)
    {
        
         _canDrag = canDrag;
    }
}
