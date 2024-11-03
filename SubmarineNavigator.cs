using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineNavigator : MonoBehaviour
{
    public Vector3 centerPoint = new Vector3(0, 0, 0); // Le point central autour duquel le sous-marin tournera
    public float radius = 5.0f; // Rayon de la trajectoire circulaire
    public float angularSpeed = 1.0f; // Vitesse angulaire (en radians par seconde)

    private float angle; // Angle actuel du sous-marin sur la trajectoire

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation de l'angle à 0
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Mise à jour de l'angle en fonction du temps et de la vitesse angulaire
        angle += angularSpeed * Time.deltaTime;

        // Calcul de la position x et z du sous-marin (rotation dans le plan XZ)
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.z + Mathf.Sin(angle) * radius;

        // Mise à jour de la position du sous-marin avec y fixe
        transform.position = new Vector3(x, centerPoint.y, z);
    }
}
