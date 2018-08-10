using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public bool m_Switch;//スイッチ
    Image m_image;

    void Start()
    {
        m_Switch = true;
        m_image = GetComponent<Image>();

        UIEventListener.Get(gameObject).onClickL = PointClick;
        UIEventListener.Get(gameObject).onPointEnter = PointEnter;
        UIEventListener.Get(gameObject).onPointExit = PointExit;

    }

    private void PointClick(GameObject go)
    {
        if (SetItem._Item != null)
        {
            GameObject tempItem = Instantiate<GameObject>(SetItem._Item);
            tempItem.transform.SetParent(go.transform.parent.parent.Find("Items"));
            tempItem.transform.position = go.transform.position;

            UIEventListener.Get(tempItem).onClickL = PointClickItemL;
            UIEventListener.Get(tempItem).onClickR = PointClickR;
            UIEventListener.Get(tempItem).onPointEnter = PointEnterItem;

        }
    }
    //Enter
    private void PointEnter(GameObject go)
    {
        if (m_Switch)
            m_image.color = Color.green;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_image.color = Color.green;
            if (SetItem._Item != null)
            {
                GameObject tempItem = Instantiate<GameObject>(SetItem._Item);
                tempItem.transform.SetParent(go.transform.parent.parent.Find("Items"));
                tempItem.transform.position = go.transform.position;

                UIEventListener.Get(tempItem).onClickL = PointClickItemL;
                UIEventListener.Get(tempItem).onClickR = PointClickR;
                UIEventListener.Get(tempItem).onPointEnter = PointEnterItem;
            }
        }
    }
    //Exit
    private void PointExit(GameObject go)
    {
        if (m_Switch)
            m_image.color = Color.white;

    }

    //Item ClickR
    private void PointClickR(GameObject go)
    {
        if (SetItem._Item == null)
        {
            Destroy(go);
        }
    }
    //Item Enter
    private void PointEnterItem(GameObject go)
    {
        if (Input.GetKey(KeyCode.Mouse1) && SetItem._Item == null)
        {
            Destroy(go);
        }
        if(Input.GetKey(KeyCode.Mouse0) && SetItem._Item != go)
        {
            go.GetComponent<Image>().sprite = SetItem._Item.GetComponent<Image>().sprite;
        }
    }
    //Item ClickL
    private void PointClickItemL(GameObject go)
    {
        if (SetItem._Item != go)
        {
            go.GetComponent<Image>().sprite = SetItem._Item.GetComponent<Image>().sprite;
        }
    }
}