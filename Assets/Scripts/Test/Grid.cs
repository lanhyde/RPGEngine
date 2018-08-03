using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public bool m_switch;//スイッチ
    Image m_image;
    private Canvas m_canvas;
    void Start()
    {

        m_switch = true;
        m_image = GetComponent<Image>();
        m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        UIEventListener.Get(gameObject).onPointEnter = PointEnter;
        UIEventListener.Get(gameObject).onPointExit = PointExit;
        UIEventListener.Get(gameObject).onBeginDrag = PointBeginDrag;

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
    public void PointBeginDrag(GameObject go)
    {
        Instantiate<GameObject>(gameObject);
       
    }


















    //public void vvv()
    //{
    //    Vector2 postion;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.transform as RectTransform,Input.mousePosition,null,out postion);


    //}
}