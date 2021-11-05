using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public ShortNote note;
    public void OnDrag(PointerEventData touchPoint)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData touchPoint)
    {
        Debug.Log("1");
        //note.ClickSnote(touchPoint);
    }

    public void OnPointerUp(PointerEventData touchPoint)
    {
        throw new System.NotImplementedException();
    }
}
