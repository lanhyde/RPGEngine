using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIEventListener : EventTrigger 
{
    public delegate void VoidDelegate(GameObject go);
    public delegate void BoolDelegate(GameObject go, bool isValue);

    public delegate void PointEnterDelegate(GameObject go);
    public delegate void PointExitDelegate(GameObject go);
    public delegate void PointBeginDownDelegate(GameObject go);

    public VoidDelegate onClick;            
    public BoolDelegate onToggleChange;     

    public PointEnterDelegate onPointEnter;
    public PointExitDelegate onPointExit;
    public PointBeginDownDelegate onBeginDrag;

    public static UIEventListener Get(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIEventListener>();
        }
        return listener;
    }
    //Button、Toggle
    public override void OnPointerClick(PointerEventData eventData) 
    {
        if(onClick != null)
        {
            onClick(gameObject);
        }
        if (onToggleChange != null)
        {
            onToggleChange(gameObject, gameObject.GetComponent<Toggle>().isOn);
        }
    }
    //PointerEnter
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(onPointEnter != null)
        {
            onPointEnter(gameObject);
        }
    }
    //PointerExit
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onPointExit != null)
        {
            onPointExit(gameObject);
        }
    }
    //BeginDrag
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(gameObject);
        }   
    }
}
