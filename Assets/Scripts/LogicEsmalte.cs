using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEsmalte : MonoBehaviour
{
    [SerializeField] GameObject piezaSinEsmaltar;

    public void FirstActivityFinished(){
        piezaSinEsmaltar.GetComponent<DragDropEsmalte>().enabled = true;
    }
}
