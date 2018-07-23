using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    private Collider[] colliders;
    private float timer = 0;
    public List<GameObject> neighbors;
    public float checkInterval = .3f;
    public float detectRadius = 10f;
    public LayerMask layersChecked;

    private void Start()
    {
        neighbors = new List<GameObject>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > checkInterval)
        {
            neighbors.Clear();
            colliders = Physics.OverlapSphere(transform.position, detectRadius, layersChecked);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Vehicle>())
                    neighbors.Add(colliders[i].gameObject);
            }
            timer = 0;
        }
    }
}
