using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotsCheckCompletion : MonoBehaviour
{
    [SerializeField] UnirPuntos[] dotsArray;
    [SerializeField] GameObject botonListo;
    bool isComplete = false;

    void Awake(){
        botonListo.SetActive(false);
    }

    public void CheckCompletion(){
        isComplete = true;
        foreach (UnirPuntos dot in dotsArray)
        {
            if (dot.IsConnected() == false) isComplete = false;
        }
        if (isComplete) botonListo.SetActive(true);
    }
}
