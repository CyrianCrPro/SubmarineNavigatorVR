using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverVR : MonoBehaviour
{
    public Transform leverBase; // Reference to the lever's pivot point
    public float minAngle = -30f; // Minimum rotation angle (X-axis)
    public float maxAngle = 30f;  // Maximum rotation angle (X-axis)

    private bool isSelected = false; // Is the lever currently grabbed?
    private Transform interactorTransform; // Transform of the VR controller grabbing the lever
    private Quaternion initialRotation; // Initial rotation of the lever
    private XRSimpleInteractable interactable;

    void Start()
    {
        // Get the XR Simple Interactable component and register the grab/release events
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelect);
            interactable.selectExited.AddListener(OnDeselect);
        }

        // Store the initial rotation of the lever
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (isSelected && interactorTransform != null)
        {
            UpdateLeverPosition();
        }
    }

    private void OnSelect(SelectEnterEventArgs arg0)
    {
        isSelected = true;
        interactorTransform = arg0.interactorObject.transform;
        Debug.Log("Lever Selected");
    }

    private void OnDeselect(SelectExitEventArgs arg0)
    {
        isSelected = false;
        interactorTransform = null;
        Debug.Log("Lever Deselected");
    }

    private void UpdateLeverPosition()
    {
        // Project the interactor's position onto the lever's plane
        Vector3 projectedInteractorPosition = ProjectPointOnPlane(leverBase.right, leverBase.position, interactorTransform.position);

        // Calculate the direction vector between the lever base and the projected hand position
        Vector3 direction = projectedInteractorPosition - leverBase.position;

        // Calculate the rotation angle around the X-axis
        float angle = Vector3.SignedAngle(leverBase.forward, direction, leverBase.right);

        // Clamp the angle within the specified limits
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Apply the constrained rotation only to the X-axis
        transform.localRotation = Quaternion.Euler(angle, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);
    }

    private Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
    {
        Vector3 toPoint = point - planePoint;
        float distance = Vector3.Dot(toPoint, planeNormal);
        return point - distance * planeNormal;
    }
}
