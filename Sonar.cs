using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] Material sonarMat;
    SubmarineNavigator navigator;

    Vector3 submarineLastPosition;
    Vector3 submarineCurrentPosition;

    void Start()
    {
        navigator = FindObjectOfType<SubmarineNavigator>();
        if (navigator == null)
        {
            Debug.LogError("SubmarineNavigator not found in the scene.");
            return;
        }

        submarineCurrentPosition = navigator.GetVirtualPosition();
        submarineLastPosition = submarineCurrentPosition;

        StartCoroutine(UpdatePositions());
    }

    IEnumerator UpdatePositions()
    {
        while (true)
        {
            float scanFrequency = sonarMat.GetFloat("_ScanFreq");
            float sonarSpeed = sonarMat.GetFloat("_ScanSpeed");

            submarineLastPosition = submarineCurrentPosition;
            submarineCurrentPosition = navigator.GetVirtualPosition();

            sonarMat.SetVector("_Position", submarineLastPosition);
            sonarMat.SetVector("_NextPosition", submarineCurrentPosition);

            Debug.Log($"Position : {submarineCurrentPosition}");

            // Réinitialiser le temps de scan pour le shader pour une synchronisation fluide
            sonarMat.SetFloat("_ScanStartTime", Time.time);

            // Attendre une petite période avant la prochaine interpolation pour une mise à jour fluide
            float interpolationTime = scanFrequency / sonarSpeed;
            yield return new WaitForSeconds(interpolationTime);
        }
    }
}
