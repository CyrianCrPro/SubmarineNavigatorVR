using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineNavigator : MonoBehaviour
{
    [SerializeField] Material sonarMat;

    // Variables pour la trajectoire circulaire
    public Vector2 centerPoint = new Vector2(0, 0); // Centre de la trajectoire du sous-marin sur le radar
    public float radius = 5.0f; // Rayon de la trajectoire circulaire
    public float angularSpeed = 1.0f; // Vitesse angulaire (en radians par seconde)

    // Position fictive pour le radar
    private Vector3 virtualPosition;
    private float angle; // Angle actuel du sous-marin sur la trajectoire

    void Start()
    {
        // Initialisation de l'angle et de la position virtuelle
        angle = 0;
        virtualPosition = CalculateVirtualPosition();
    }

    void Update()
    {
        // Mise à jour de l'angle en fonction du temps et de la vitesse angulaire
        angle += angularSpeed * Time.deltaTime;

        // Calcul de la position fictive en suivant une trajectoire circulaire
        virtualPosition = CalculateVirtualPosition();
    }

    // Calcule la position fictive sur la trajectoire circulaire
    private Vector3 CalculateVirtualPosition()
    {
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.y + Mathf.Sin(angle) * radius;
        return new Vector3(x, y, transform.position.z);
    }

    // Méthode pour récupérer la position virtuelle pour le radar
    public Vector3 GetVirtualPosition()
    {
        return virtualPosition;
    }
}
