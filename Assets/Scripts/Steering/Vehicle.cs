using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : SteerableGameEntity {
    private CharacterController controller;
    private Rigidbody theRigidbody;
    private Vector3 moveDistance;

    public override void OnInitialize()
    {
        controller = GetComponent<CharacterController>();
        theRigidbody = GetComponent<Rigidbody>();
        moveDistance = Vector3.zero;
        base.OnInitialize();
    }

    private void Update()
    {
        base.Refresh(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Velocity += acceleration * Time.fixedDeltaTime;

        if (Velocity.sqrMagnitude > SqrMaxSpeed)
            Velocity = Velocity.normalized * MaxSpeed;
        moveDistance = Velocity * Time.fixedDeltaTime;

        if(IsPlanar)
        {
            Velocity.y = 0;
            moveDistance.y = 0;
        }

        if (controller != null)
        {
            controller.SimpleMove(Velocity);
        }
        else if (theRigidbody == null || theRigidbody.isKinematic)
        {
            transform.position += moveDistance;
        }
        else
        {
            theRigidbody.MovePosition(theRigidbody.position + moveDistance);
        }

        if(Velocity.sqrMagnitude > 0.00001)
        {
            Vector3 newForward = Vector3.Lerp(transform.forward, Velocity, Damping * Time.fixedDeltaTime);
            if(IsPlanar)
            {
                newForward.y = 0;
            }
            transform.forward = newForward;
        }
    }
}
