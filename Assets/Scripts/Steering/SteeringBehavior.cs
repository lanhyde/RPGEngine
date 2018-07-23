using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour {
    public float weight = 1f;

    public virtual Vector3 Force()
    {
        return Vector3.zero;
    }
}
