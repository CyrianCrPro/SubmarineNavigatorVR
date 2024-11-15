//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class LeverLookAt : MonoBehaviour
//{
//    public Transform debugLookAtTarget;
//    XRSimpleInteractable interactable;
//    public bool isSelected;
//    // Start is called before the first frame update
//    void Start()
//    {
//        interactable = GetComponent<XRSimpleInteractable>();
//        interactable.selectEntered.AddListener(OnSelect);
//    }

//    private void OnSelect(SelectEnterEventArgs arg0)
//    {
//        isSelected = true;
//        Debug.Log("Selected");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 projectedPos = transform.TransformPoint(debugLookAtTarget.position.position);
//        projectedPos.y = 0;
//        Vector3 targetPos = transform.InverseTransformPoint(projectedPos);
//        transform.LookAt(debugLookAtTarget.position);
//    }

//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.blue;
//        Gizmos.DrawSphere(targetPos,0.1f);
//        Gizmos.DrawLine(transform.position, targetPos);
//    }

//}
