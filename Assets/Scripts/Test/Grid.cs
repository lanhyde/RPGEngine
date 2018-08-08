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

        UIEventListener.Get(gameObject).onClickL = PointClick;
        UIEventListener.Get(gameObject).onPointEnter = PointEnter;
        UIEventListener.Get(gameObject).onPointExit = PointExit;
    }

    private void PointClick(GameObject go)
    {
        if(SetItem._item != null)
        {
            GameObject tempItem = Instantiate<GameObject>(SetItem._item);
            tempItem.transform.SetParent(go.transform.parent.parent.Find("Items"));
            tempItem.transform.position = go.transform.position;
            UIEventListener.Get(tempItem).onClickR = PointClickR;
        }
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


    private void PointClickR(GameObject go)
    {
        if (SetItem._item == null)
        {
            Destroy(go);
        }
    }
}