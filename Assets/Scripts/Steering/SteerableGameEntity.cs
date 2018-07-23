using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class SteerableGameEntity : BaseGameEntity {
    public float MaxSpeed = 10f;
    public float MaxForce = 100f;
    
    public float SqrMaxSpeed {
        get
        {
            return MaxSpeed * MaxSpeed;
        }
    }
    public float Mass = 1f;
    public Vector3 Velocity;
    public float Damping = 0.9f;
    public float ComputeInterval = 0.2f;
    public bool IsPlanar = true;
    public Vector3 SteeringForce;

    protected Vector3 acceleration;

    private float timer;
    private SteeringBehavior[] steerings;
    public override void OnInitialize()
    {
        base.OnInitialize();
        SteeringForce = Vector3.zero;
        timer = 0;
        steerings = GetComponents<SteeringBehavior>();
    }

    public override bool HandleMessage(Telegram msg)
    {
        return false;
    }

    public override void Refresh(float deltaTime)
    {
        timer += Time.deltaTime;
        SteeringForce = Vector3.zero;

        if(timer > ComputeInterval)
        {
            foreach (var s in steerings)
            {
                if (s.enabled)
                    SteeringForce += s.Force() * s.weight;
            }
            SteeringForce = Vector3.ClampMagnitude(SteeringForce, MaxForce);
            acceleration = SteeringForce / Mass;
            timer = 0f;
        }
    }
}
