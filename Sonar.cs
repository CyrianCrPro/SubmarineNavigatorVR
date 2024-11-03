using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    // matériau pour pouvoir envoyer au sous marin, et du coup au shader, la position du sous marin
    [SerializeField] Material sonarMat; // champ sérialisé, donc visible dans l'inspecteur mais privé
    SubmarineNavigator navigator;

    // Start is called before the first frame update
    void Start()
    {
        navigator = FindObjectOfType<SubmarineNavigator>();

        StartCoroutine(UpdatePositions());
    }

    // récupérer fréquence de scan, la vitesse (dans le shader)
    // quand time*speed arrive au dela de scan freq => ça revient à 0 (faire time -= threshold) (pas = 0 sinon on perd des millisecondes, et donc pas synchro) 
    void Update()
    {
        if ()
        {
            StartCoroutine(UpdatePositions());
        }
    }

    //IEnumerator UpdatePositions()
    //{
    //    float scanFrequency = 1;
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(scanFrequency);
    //        // update les positions
    //        // navigator.submarinePosition ...
    //        // passer la nextPos en pos, et mettre une nouvelle nextPos

    //        //sonarMat.SetVector("_Position", submarineLastPosition); récupérer les positions de submarine navigator
    //        //sonarMat.SetVector("_NextPosition", submarinePosition);
    //    }
    //}

    IEnumerator UpdatePositions()
    {
        
        yield return null;
        // update les positions
        // navigator.submarinePosition ...
        // passer la nextPos en pos, et mettre une nouvelle nextPos

        //sonarMat.SetVector("_Position", submarineLastPosition); récupérer les positions de submarine navigator
        //sonarMat.SetVector("_NextPosition", submarinePosition);
        }
    }
}