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
        UIEventListener.Get(gameObject).onPointDrop = PointDrop;

    }

    //Enter
    private void PointEnter(GameObject go)
    {
        if (m_switch)
            m_image.color = Color.green;
    }
    //Exit
    private void PointExit(GameObject go)
    {
        if (m_switch)
            m_image.color = Color.white;
    }

    private void PointDrop(GameObject go)
    {
        if (SetItem.m_item != null)
        {
            SetItem.m_item = null;
        }
 
    }







}