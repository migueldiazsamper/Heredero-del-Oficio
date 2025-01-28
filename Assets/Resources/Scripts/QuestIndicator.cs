using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicador;
    private void Update()
    {
        switch (PhasesManager.instance.currentQuest)
        {
            case "Fabrica":
                indicador.transform.position = new Vector2(1.41f, 98.78f);
                break;
            case "Mina":
                indicador.transform.position = new Vector2(-8.6f, 48.4f);
                break;
            case "Hornos":
                indicador.transform.position = new Vector2(27.4f, 117.1f);
                break;
            case "Viejo":
                indicador.transform.position = new Vector2(14.8f, 70.2f);
                break;
            case "Tienda":
                indicador.transform.position = new Vector2(35.8f, 69.8f);
                break;
            case "Palacio":
                indicador.transform.position = new Vector2(52.1f, 98.2f);
                break;
        }
    }

}
