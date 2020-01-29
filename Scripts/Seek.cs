using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic character; // pulibc? what's the matter with you?
    public GameObject target; // shut it I'm just trying to get this working ye great ape

    public bool flee = false;

    public float maxAcceleration = 100f;

    public virtual SteeringOutput getSteering()
    {
        target = GameObject.Find("Player");
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        // Determine if its seek or flee
        if (flee)
        {
            result.linear = character.transform.position - target.transform.position;

        }
        else
        {
            result.linear = target.transform.position - character.transform.position;

        }

        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;

        return result;
    }
}
