using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerShake : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
    public IEnumerator TriggerShake(ActionBasedController controller)
    {
        if (controller != null && controller.enableInputActions)
        {
            controller.SendHapticImpulse(intensity, duration);
            yield return new WaitForSeconds(duration);
        }
    }
}