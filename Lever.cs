using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lever : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private HingeJoint hjoint;

    void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        hjoint = GetComponent<HingeJoint>();

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Lever Grabbed");
        // Additional logic when the lever is grabbed
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Lever Released");
        // Additional logic when the lever is released
    }

    void Update()
    {
        // Optional: Read the current angle of the lever
        float angle = hjoint.angle;
        // Implement any logic based on the lever's angle
    }
}


