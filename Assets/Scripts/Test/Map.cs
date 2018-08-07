using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public GameObject m_grid;

    public RectTransform m_map; //base map

    public List<GameObject> m_grids;

    public Transform m_canvas;
    void Start ()
    {
        m_map = GetComponent<RectTransform>();
        int mapWidth = (int)m_map.sizeDelta.x;
        int mapHeight = (int)m_map.sizeDelta.y;

        RectTransform grid = m_grid.GetComponent<RectTransform>();
        int gridWidth = (int)grid.sizeDelta.x;
        int gridHeight = (int)grid.sizeDelta.y;


        for (int i = 0; i < (mapWidth / gridWidth) * (mapHeight / gridHeight); i++)//print map
        {
            GameObject temp = Instantiate<GameObject>(m_grid);
            temp.name = "Grid" + i;
            temp.transform.SetParent(transform.Find("Group"));
            temp.AddComponent<Grid>();
            m_grids.Add(temp);
        }
    }


}
