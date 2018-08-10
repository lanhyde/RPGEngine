using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public GameObject m_Grid;

    public RectTransform m_Map; //base map

    public List<GameObject> m_Grids;

    void Start ()
    {
        m_Map = GetComponent<RectTransform>();
        int mapWidth = (int)m_Map.sizeDelta.x;
        int mapHeight = (int)m_Map.sizeDelta.y;

        RectTransform grid = m_Grid.GetComponent<RectTransform>();
        int gridWidth = (int)grid.sizeDelta.x;
        int gridHeight = (int)grid.sizeDelta.y;

        for (int i = 0; i < (mapWidth / gridWidth) * (mapHeight / gridHeight); i++)//print map
        {
            GameObject temp = Instantiate<GameObject>(m_Grid);
            temp.name = "Grid" + i;
            temp.transform.SetParent(transform.Find("Group"));
            temp.AddComponent<Grid>();
            m_Grids.Add(temp);
        }
    }

}
