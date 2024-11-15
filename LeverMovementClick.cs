using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverMovement : MonoBehaviour
{
    XRBaseInteractable Interactable;

    private bool isRotatedPositive = false;
    private Quaternion positiveRotation;
    private Quaternion negativeRotation;

    void Start()
    {
        Interactable = GetComponent<XRBaseInteractable>();
        Interactable.selectEntered.AddListener(OnSelect);

        positiveRotation = Quaternion.Euler(30f, transform.localEulerAngles.y, transform.localEulerAngles.z);
        negativeRotation = Quaternion.Euler(-30f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void OnSelect(SelectEnterEventArgs arg0)
    {
        isRotatedPositive = !isRotatedPositive;
        transform.localRotation = isRotatedPositive ? positiveRotation : negativeRotation;

        Debug.Log("Lever rotated to " + (isRotatedPositive ? "+30" : "-30") + " degrees on X axis");
    }
}
