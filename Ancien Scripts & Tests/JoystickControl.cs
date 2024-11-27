using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField]
    private float forwardBackwardTilt = 0;


    // Update is called once per frame
    void Update()
    {
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Math.Abs(forwardBackwardTilt - 360);
            Debug.Log("Backward"+ forwardBackwardTilt);
        }
        else if (forwardBackwardTilt > 5 &&  forwardBackwardTilt < 74)
        {
            Debug.Log("Forward"+ forwardBackwardTilt);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            transform.LookAt(transform.position, transform.up);
        }
    }
}
