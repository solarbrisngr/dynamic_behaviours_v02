using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : Seek {

    float maxSpeed = 100f;
    float slowRadius = 3f;

    float targetRadius = 1.5f;

    float timeToTarget = .1f;

    public override SteeringOutput getSteering()
    {
        target = GameObject.Find("Player");
        SteeringOutput result = new SteeringOutput();

        float distance = target.transform.position.z - character.transform.position.z;

        distance = Mathf.Abs(distance);
        Debug.Log(distance);

        if (distance < targetRadius)
        {
            return null;
        }

        if (distance > slowRadius)
        {
            result.linear = target.transform.position - character.transform.position;

            result.linear.Normalize();
            result.linear *= maxAcceleration;
            return result;
        }
        else
        {
            result.linear.z = maxSpeed * (distance / slowRadius);

            Vector3 targetVelocity = new Vector3();
            targetVelocity = result.linear;
            targetVelocity.Normalize();
            targetVelocity *= result.linear.z;

            result.linear = targetVelocity - character.linearVelocity;
            result.linear /= timeToTarget;

            if(result.linear.magnitude > maxAcceleration)
            {
                result.linear.Normalize();
                result.linear *= maxAcceleration;
            }

            result.angular = 0;
            return result;
        }

        
    }
}
