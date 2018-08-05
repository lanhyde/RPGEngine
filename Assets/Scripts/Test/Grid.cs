using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public bool m_switch;//スイッチ
    Image m_image;

    void Start()
    {
        m_switch = true;
        m_image = GetComponent<Image>();

        UIEventListener.Get(gameObject).onPointEnter = PointEnter;
        UIEventListener.Get(gameObject).onPointExit = PointExit;
        UIEventListener.Get(gameObject).onBeginDrag = PointerDown;
    }

    //Enter
    public void PointEnter(GameObject go)
    {
        if (m_switch)
            m_image.color = Color.green;
    }
    //Exit
    public void PointExit(GameObject go)
    {
        if (m_switch)
            m_image.color = Color.white;
    }

    //Drag
    public void PointerDown(GameObject go)
    {
        GameObject temp = Instantiate<GameObject>(gameObject);
    }
















    //public void vvv()
    //{
    //    Vector2 postion;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.transform as RectTransform,Input.mousePosition,null,out postion);
    //}
}