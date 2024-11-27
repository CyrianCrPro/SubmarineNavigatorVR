using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveMovement : MonoBehaviour
{
    [Header("Controle du sous-marin")]
    public SubmarineNavigator submarineNavigator; // reference le script SubmarineNavigator

    [Header("Paramètres de la Manivelle")]
    public float rotationSpeed = 500f;

    [Header("Debug")]
    public bool isSelected = false;

    private float totalRotation = 0f; // rotations accumulés de la valve

    private float radius; // angle actuel du sous-marin

    private XRSimpleInteractable interactable;
    private InputDevice leftController;
    void Start()
    {

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
        else
        {
            Debug.LogError("Left-hand controller not found.");
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

        bool buttonX = leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aPressed) && aPressed; // X button
        bool buttonY = leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bPressed) && bPressed; // Y button


        if (submarineNavigator != null)
        {
            radius = Mathf.Clamp(totalRotation * 1f, -45f, 45f);
        }

        if (buttonX && !buttonY)
        {
            RotateCrank(-1);
        }
        else if (buttonY && !buttonX)
        {
            RotateCrank(1);
        }

        // rotation totale -> angle du sous-marin
        if (submarineNavigator != null)
        {
            submarineNavigator.UpdateRadius(totalRotation);
        }
    }

    void RotateCrank(int direction)
    {
        // valeur de la rotation en une frame selon la direction et vitesse (rotation indépendant des frames rates avec Time.deltaTime
        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        float newTotalRotation = totalRotation + rotationAmount;


        // rotate la valve (objet) et vérifie que la prochaine rotaton est dans les limites
        if (newTotalRotation * 0.01f >= -2f && newTotalRotation * 0.01f <= 2f)
        {
            // rotate la valve (objet)
            transform.Rotate(Vector3.up * rotationAmount);

            // on garde la rotation totale
            totalRotation = newTotalRotation;
        }
    }

    private void OnSelect(SelectEnterEventArgs arg0)
    {
        isSelected = true;
        Debug.Log("Valve Selected");
    }

    private void OnDeselect(SelectExitEventArgs arg0)
    {
        isSelected = false;
        Debug.Log("Valve Deselected");
    }
}