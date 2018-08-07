using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItem : MonoBehaviour {

    public Transform[] itemPostions;
    public static GameObject m_item;
    private void Start()
    {
        for(int i = 0; i < itemPostions.Length; i++)
        {
            GameObject tempGameObj = Instantiate(Resources.Load("Perfab/Item")) as GameObject;
            tempGameObj.transform.SetParent(transform.Find("0" + i.ToString()).transform);
            tempGameObj.transform.position = itemPostions[i].position;
            tempGameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/0" + i.ToString());

            UIEventListener.Get(tempGameObj).onPointEnter = PointEnter;
            //UIEventListener.Get(tempGameObj).onPointBeginDrag = PointBeginDrag;
        }

        m_item = null;

    }

    private void PointEnter(GameObject go)
    {
        if (m_item == null)
        {
            m_item = Instantiate<GameObject>(go);
            m_item.transform.SetParent(transform.parent);
            m_item.transform.position = go.transform.position;

            UIEventListener.Get(m_item).onPointBeginDrag = PointBeginDrag;
            UIEventListener.Get(m_item).onPointDrag = PointDrag;
            UIEventListener.Get(m_item).onPointDragEnd = PointDragEnd;
        }
    }


    //private void PointExit(GameObject go)
    //{
    //    Destroy(go);
    ////}

    private void PointBeginDrag(GameObject go)
    {
    
    }  
    private void PointDrag(GameObject go)
    {

    }
    private void PointDragEnd(GameObject go)
    {
     
    }

}
