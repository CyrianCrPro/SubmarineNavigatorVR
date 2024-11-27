using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverMovementClick : MonoBehaviour
{
    XRBaseInteractable Interactable;
    private bool isRotatedPositive = false;

    [SerializeField] private SubmarineNavigator submarineNavigator;
    [SerializeField] private float positiveSpeed = 1f; 
    [SerializeField] private float neutralSpeed = 0f;
    [SerializeField] private float negativeSpeed = -1f; 

    private Quaternion positiveRotation;
    private Quaternion neutralRotation;
    private Quaternion negativeRotation;

    void Start()
    {
        Interactable = GetComponent<XRBaseInteractable>();
        Interactable.selectEntered.AddListener(OnSelect);

        positiveRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 30f);
        neutralRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
        negativeRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, -30f);


        if (submarineNavigator == null)
        {
            Debug.LogError("SubmarineNavigator is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.localRotation = positiveRotation;
            submarineNavigator.SetSpeed(positiveSpeed);
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.localRotation = neutralRotation;
            submarineNavigator.SetSpeed(neutralSpeed);
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.localRotation = negativeRotation;
            submarineNavigator.SetSpeed(negativeSpeed);
        }
    }

    private void OnSelect(SelectEnterEventArgs arg0)
    {
        isRotatedPositive = !isRotatedPositive;
        transform.localRotation = isRotatedPositive ? positiveRotation : negativeRotation;

        if (submarineNavigator != null)
        {
            float newSpeed = isRotatedPositive ? positiveSpeed : negativeSpeed;
            submarineNavigator.SetSpeed(newSpeed);
        }

        Debug.Log("Lever rotated to " + (isRotatedPositive ? "+30" : "-30") + " degrees on X axis");
    }
}
