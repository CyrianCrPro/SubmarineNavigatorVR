using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestSelect : MonoBehaviour
{

    XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.onSelectEntered.AddListener(OnSelect);
    }

    void OnSelect(XRBaseInteractor args)
    {
        Debug.Log("Input détecté !");
    }

}
