using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMovement : MonoBehaviour
{
    // Define rotations
    private Quaternion freinRotation = Quaternion.Euler(-30f, 0f, 0f);
    private Quaternion neutreRotation = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion avancerRotation = Quaternion.Euler(30f, 0f, 0f);

    // Reference to SubmarineNavigator
    public SubmarineNavigator submarineNavigator;

    // Speeds for different lever positions
    public float forwardSpeed = 2.0f;
    public float neutralSpeed = 0.0f;
    public float reverseSpeed = -1.0f;

    void Start()
    {
        transform.rotation = neutreRotation;

        // If submarineNavigator is not set in the Inspector, try to find it
        if (submarineNavigator == null)
        {
            submarineNavigator = GameObject.FindObjectOfType<SubmarineNavigator>();
            if (submarineNavigator == null)
            {
                Debug.LogError("SubmarineNavigator not found in the scene.");
            }
        }
    }

    void Update()
    {
        bool isUpArrowPressed = Input.GetKey(KeyCode.UpArrow);
        bool isDownArrowPressed = Input.GetKey(KeyCode.DownArrow);

        if (isUpArrowPressed && isDownArrowPressed)
        {
            // Move to Neutre position
            transform.rotation = neutreRotation;
            // Set angularSpeed to neutral
            submarineNavigator.angularSpeed = neutralSpeed;
        }
        else if (isUpArrowPressed)
        {
            // Move to Avancer position
            transform.rotation = avancerRotation;
            // Set angularSpeed to forward speed
            submarineNavigator.angularSpeed = forwardSpeed;
        }
        else if (isDownArrowPressed)
        {
            // Move to Frein/Reculer position
            transform.rotation = freinRotation;
            // Set angularSpeed to reverse speed
            submarineNavigator.angularSpeed = reverseSpeed;
        }
        else
        {
            // Default to Neutre position
            transform.rotation = neutreRotation;
            // Set angularSpeed to neutral
            submarineNavigator.angularSpeed = neutralSpeed;
        }
    }
}
