using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public Material sonarMat;
    SubmarineNavigator navigator;

    // Start is called before the first frame update
    void Start()
    {
        navigator = FindObjectOfType<SubmarineNavigator>();
    }

    IEnumerator UpdatePositions()
    {
        float scanFrequency = 1;
        while (true)
        {
            yield return new WaitForSeconds(1);
            // update les positions
            // navigator.submarinePosition ...
            // passer la nextPos en pos, et mettre une nouvelle nextPos
        }
    }
}