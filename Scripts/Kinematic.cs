using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position

    
    
    // Finds the current player object in the seen
    public GameObject myTarget;
    

    // Flags for different behaviours
    public bool flee = false;
    public bool align = false;
    public bool face = false;
    public bool look = false;
    public bool arrive = false;
    public bool seek = false;


    // Update is called once per frame
    void Update()
    {
        myTarget = GameObject.Find("Player");
        // update my position and rotation
        this.transform.position += linearVelocity * Time.deltaTime;
        Vector3 v = new Vector3(0, angularVelocity, 0); // TODO - don't make a new Vector3 every update you silly person
        this.transform.eulerAngles += v * Time.deltaTime;

        // update linear and angular velocities
        SteeringOutput steering = new SteeringOutput();

        //TODO add switch statement for different move type (6 behaviours)

        if (flee)
        {
            Seek mySeek = new Seek
            {
                character = this,
                target = myTarget
            };
            mySeek.flee = true;
            steering = mySeek.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        else if (seek)
        {
            Seek mySeek = new Seek
            {
                character = this,
                target = myTarget
            };
            steering = mySeek.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }

        else if (arrive)
        {
            Arrive myArrive = new Arrive
            {
                character = this,
                target = myTarget
            };
            steering = myArrive.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
            else
            {
                linearVelocity = Vector3.zero;
            }
        }

        else if (face)
        {
            Face myAlign = new Face
            {
                character = this,
                target = myTarget
            };
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        else if (look)
        {
            LookWhereYoureGoing myAlign = new LookWhereYoureGoing
            {
                character = this,
                target = myTarget
            };
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        else if (align)
        {
            Align myAlign = new Align
            {
                character = this,
                target = myTarget
            };
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }





    }

    // a stub to test my update function. In the future we will call getSteering on different dynamic steering behavior classes
    //SteeringOutput getSteering()
    //{
    //    SteeringOutput  steering = new SteeringOutput();
    //    steering.linear.z = 0;
    //    steering.angular = 1;
    //    return steering;
    //}


}
