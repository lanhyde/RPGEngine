using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItem : MonoBehaviour {

    public Transform[] itemPostions;

    private GameObject m_item;
    public static GameObject _item;

    private void Start()
    {
        for(int i = 0; i < itemPostions.Length; i++)
        {
            GameObject tempGameObj = Instantiate(Resources.Load("Perfab/Item")) as GameObject;
            tempGameObj.transform.SetParent(transform.Find("0" + i.ToString()).transform);
            tempGameObj.transform.position = itemPostions[i].position;
            tempGameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/0" + i.ToString());

            UIEventListener.Get(tempGameObj).onClickL = PointDownL;
        }

        m_item = null;

    }

    private void PointDownL(GameObject go)
    {
        if (m_item == null)
        {
            m_item = Instantiate<GameObject>(go);
            m_item.transform.SetParent(transform.parent);
            m_item.transform.position = go.transform.position;

            _item = go;

            Image itemImage = m_item.GetComponent<Image>();
            Color c = itemImage.color;
            c.a = 0.5f;
            itemImage.color = c;
            itemImage.raycastTarget = false;

            StartCoroutine(ItemOrMouse(itemImage));
        }
    }

    public IEnumerator ItemOrMouse(Image image)
    {
        Vector3 mousePos;
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                image.raycastTarget = true;
                Destroy(m_item);
                //yield return new WaitForSeconds(0.5f);
                m_item = null;
                _item = null;
                StopCoroutine(ItemOrMouse(image));
            }
            if (m_item != null)
            {
                mousePos = Input.mousePosition;
                m_item.transform.position = mousePos;
            }
            yield return new WaitForSeconds(0.01f);
         
        }
    }


}
