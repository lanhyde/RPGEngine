using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIEventListener : EventTrigger 
{

    public delegate void MouseL(GameObject go);// +
    public delegate void MouseR(GameObject go);// -

    public delegate void BoolDelegate(GameObject go, bool isValue);

    public delegate void PointEnterDelegate(GameObject go); //1  Enter
    public delegate void PointExitDelegate(GameObject go);  //2  Exit
    public delegate void PointBeginDrag(GameObject go);     //3  BeginDrag
    public delegate void PointDrag(GameObject go);          //4  Drag
    public delegate void PointDragEnd(GameObject go);       //5  DragEnd
    public delegate void PointDrop(GameObject go);          //6  Drop


    public MouseL onClickL;
    public MouseR onClickR;

    public BoolDelegate onToggleChange;     

    public PointExitDelegate onPointExit;   //1  Exit
    public PointEnterDelegate onPointEnter; //2  Enter
    public PointBeginDrag onPointBeginDrag;      //3  BeginDrag
    public PointDrag onPointDrag;           //4  Drag
    public PointDragEnd onPointDragEnd;     //5  DragEnd
    public PointDrop onPointDrop;           //6  Drop

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
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (onClickL != null)
                {
                    onClickL(gameObject);
                }
                break;
            case PointerEventData.InputButton.Right:
                if (onClickR != null)
                {
                    onClickR(gameObject);
                }
                break;
        }

      
        if (onToggleChange != null)
        {
            onToggleChange(gameObject, gameObject.GetComponent<Toggle>().isOn);
            print("Pointer Click   Toggle Change");
        }
    }



    //1  PointerEnter
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(onPointEnter != null)
        {
            onPointEnter(gameObject);
            //print("Pointer Enter");
        }
    }
    //2  PointerExit
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onPointExit != null)
        {
            onPointExit(gameObject);
            //print("Pointer Exit");
        }
    }
    //3  BeginDrag
    public override void OnBeginDrag(PointerEventData eventData)
    {

    }
    //4  Drag
    public override void OnDrag(PointerEventData eventData)
    {

    }
    //5  DragEnd
    public override void OnEndDrag(PointerEventData eventData)
    {

    }
    //6  Drop
    public override void OnDrop(PointerEventData eventData)
    {

    }
 
}
