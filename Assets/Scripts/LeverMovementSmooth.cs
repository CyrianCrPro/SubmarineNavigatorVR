using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverMovementSmooth : MonoBehaviour
{
    [Header("Controle du sous-marin")]
    [SerializeField] private SubmarineNavigator submarineNavigator;
    [SerializeField] private float maxSpeed = 0.05f; // vitesse max du sous-marin

    [Header("Paramètres du Levier")]
    [SerializeField] private float rotationStep = 30f; // rotation max (en degrés)
    [SerializeField] private float moveSpeed = 50f; // vitesse du mouvement du levier

    [Header("XR Controller Droit")]
    private InputDevice rightController;

    private float currentRotation = 0f; // rotation de base du levier
    [Header("Debug")]
    public bool isSelected = false;

    private XRSimpleInteractable interactable;

    void Start()
    {
        if (submarineNavigator == null)
        {
            Debug.LogError("SubmarineNavigator is not assigned.");
        }

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
        else
        {
            Debug.LogError("Right-hand controller not found.");
        }

        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelect);
            interactable.selectExited.AddListener(OnDeselect);
        }
    }

    void Update()
    {

        if (!isSelected) return;
        //bool buttonA = rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aPressed) && aPressed; // A button
        //bool buttonB = rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bPressed) && bPressed; // B button


        //// Mathf.Clamp = rester dans les limites d'angle du levier
        //if (buttonB && !buttonA)
        //{
        //    // glissement fluide du levier vers le max
        //    currentRotation = Mathf.Clamp(currentRotation + moveSpeed * Time.deltaTime, -rotationStep, rotationStep);
        //    // donne un ratio -1/1
        //    submarineNavigator.SetSpeed(maxSpeed * (currentRotation / rotationStep));
        //}
        //else if (buttonA && buttonB)
        //{
        //    // glissement fluide du levier vers le neutre
        //    if (currentRotation > 0)
        //    {
        //        currentRotation = Mathf.Clamp(currentRotation - moveSpeed * Time.deltaTime, 0f, rotationStep);
        //    }
        //    else if (currentRotation < 0)
        //    {
        //        currentRotation = Mathf.Clamp(currentRotation + moveSpeed * Time.deltaTime, -rotationStep, 0f);
        //    }
        //    submarineNavigator.SetSpeed(0f);
        //}
        //else if (buttonA && !buttonB)
        //{
        //    // glissement fluide du levier vers le min
        //    currentRotation = Mathf.Clamp(currentRotation - moveSpeed * Time.deltaTime, -rotationStep, rotationStep);
        //    submarineNavigator.SetSpeed(maxSpeed * (currentRotation / rotationStep));
        //}

        //// on applique la rotation sur le levier
        //transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotation);
        //}

        // Partie du code à (dé)-commenter si on n'utilise pas les manettes mais le numpad.
        //////////////////////////
        
        if (Input.GetKey(KeyCode.Keypad8))
        {
            // glissement fluide du levier vers le max
            currentRotation = Mathf.Clamp(currentRotation + moveSpeed * Time.deltaTime, -rotationStep, rotationStep);
            // donne un ratio -1/1
            submarineNavigator.SetSpeed(maxSpeed * (currentRotation / rotationStep));
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {
            // glissement fluide du levier vers le neutre
            if (currentRotation > 0)
            {
                currentRotation = Mathf.Clamp(currentRotation - moveSpeed * Time.deltaTime, 0f, rotationStep);
            }
            else if (currentRotation < 0)
            {
                currentRotation = Mathf.Clamp(currentRotation + moveSpeed * Time.deltaTime, -rotationStep, 0f);
            }
            submarineNavigator.SetSpeed(0f);
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            // glissement fluide du levier vers le min
            currentRotation = Mathf.Clamp(currentRotation - moveSpeed * Time.deltaTime, -rotationStep, rotationStep);
            submarineNavigator.SetSpeed(maxSpeed * (currentRotation / rotationStep));
        }

        // on applique la rotation sur le levier
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotation);
    }
    //////////////////////////

    private void OnSelect(SelectEnterEventArgs arg0)
    {
        isSelected = true;
        Debug.Log("Lever Selected");
    }

    private void OnDeselect(SelectExitEventArgs arg0)
    {
        isSelected = false;
        Debug.Log("Lever Deselected");
    }
}