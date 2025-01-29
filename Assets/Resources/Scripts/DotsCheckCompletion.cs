using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotsCheckCompletion : MonoBehaviour
{
    public UnirPuntos[] dotsArray;
    [SerializeField] GameObject botonListo;
    public int numberOfDots;
    [SerializeField] GameObject mancerinaFinal;
    bool isComplete = false;

    [ SerializeField ] Animator bienHechoAnimator;
    [ SerializeField ] GameObject bienHecho;

    void Awake(){
        botonListo.SetActive(false);
        dotsArray = new UnirPuntos[numberOfDots];
        for(int i = 0; i < numberOfDots; i++){
            dotsArray[i] = GameObject.Find("ID " + i).GetComponent<UnirPuntos>();
        }
        mancerinaFinal.SetActive(false);
        bienHecho.SetActive(false);
    }

    public void CheckCompletion(){
        isComplete = true;
        foreach (UnirPuntos dot in dotsArray)
        {
            if (dot.IsConnected() == false) isComplete = false;
        }
        if (isComplete) {
            
            StartCoroutine(ShowMancerinaFinal());
        }
    }

    private IEnumerator ShowMancerinaFinal(){
        yield return new WaitForSeconds(0.5f);
        botonListo.SetActive(true);
        for(int i = 0; i < numberOfDots; i++)
        {
            GameObject.Find("ID " + i).SetActive(false);
        }
        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().linkPoint, AudioManager.GetInstance().linkPointVolume);
        mancerinaFinal.SetActive(true);

        if ( ! bienHecho.activeSelf )
        {
            bienHecho.SetActive(true);
            bienHechoAnimator.SetTrigger("BienHecho");
        }
    }
}
