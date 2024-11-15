using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverMovement : MonoBehaviour
{
    XRBaseInteractable Interactable;
    private bool isRotatedPositive = false; // Track the current rotation state
    private Quaternion positiveRotation;
    private Quaternion negativeRotation;

    private Transform handTransform; // Reference to the hand transform
    private Vector3 initialHandPosition;
    private Quaternion initialHandRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Set up the XR Simple Interactable
        Interactable = GetComponent<XRBaseInteractable>();
        if (Interactable != null)
        {
            Interactable.selectEntered.AddListener(OnSelect);
            Interactable.selectExited.AddListener(OnDeselect);
        }

        // Define the two rotation states for the lever
        positiveRotation = Quaternion.Euler(30f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        negativeRotation = Quaternion.Euler(-30f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        // Lock hand to lever position
        handTransform = args.interactorObject.transform;
        initialHandPosition = handTransform.position;
        initialHandRotation = handTransform.rotation;

        // Lock the hand to the lever's position
        handTransform.position = transform.position;
        handTransform.rotation = transform.rotation;

        // Toggle lever rotation
        isRotatedPositive = !isRotatedPositive;
        transform.localRotation = isRotatedPositive ? positiveRotation : negativeRotation;

        Debug.Log("Lever rotated to " + (isRotatedPositive ? "+30" : "-30") + " degrees on X axis");
    }

    private void OnDeselect(SelectExitEventArgs args)
    {
        // Release hand from the lever (restore initial position if desired)
        if (handTransform != null)
        {
            handTransform.position = initialHandPosition;
            handTransform.rotation = initialHandRotation;
            handTransform = null;
        }
    }
}
