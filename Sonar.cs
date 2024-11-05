using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] Material sonarMat;
    SubmarineNavigator navigator;

    // Variables pour suivre les positions sur le radar
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

        // Initialisation des positions
        submarineCurrentPosition = navigator.GetVirtualPosition();
        submarineLastPosition = submarineCurrentPosition;

        // Lancement de la coroutine pour mettre à jour les positions
        StartCoroutine(UpdatePositions());
    }

    IEnumerator UpdatePositions()
    {
        while (true)
        {
            // Récupérer la fréquence de scan depuis le matériau du sonar
            float scanFrequency = sonarMat.GetFloat("_ScanFreq");

            // Attendre la durée définie par scanFrequency
            yield return new WaitForSeconds(scanFrequency);

            // Mise à jour de la dernière position
            submarineLastPosition = submarineCurrentPosition;

            // Mise à jour de la position actuelle du sous-marin sur le radar
            submarineCurrentPosition = navigator.GetVirtualPosition();

            // Transfert des positions au shader
            sonarMat.SetVector("_Position", submarineLastPosition);
            sonarMat.SetVector("_NextPosition", submarineCurrentPosition);

            Debug.Log($"Position : {submarineCurrentPosition}");

            // Récupération de la vitesse du sonar depuis le matériau
            float sonarSpeed = sonarMat.GetFloat("_ScanSpeed");

            // Mise à jour du temps de début du scan dans le shader pour synchronisation
            sonarMat.SetFloat("_ScanStartTime", Time.time);

        }
    }
}
