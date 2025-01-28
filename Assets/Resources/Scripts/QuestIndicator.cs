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
                indicador.transform.position = new Vector2(3.66f, 99.37f);
                break;
            case "Mina":
                indicador.transform.position = new Vector2(-8.6f, 48.4f);
                break;
            case "Hornos":
                indicador.transform.position = new Vector2(28.81f, 114.11f);
                break;
            case "Viejo":
                indicador.transform.position = new Vector2(43.09f, 45.31f);
                break;
            case "Tienda":
                indicador.transform.position = new Vector2(31.94f, 70.88f);
                break;
            case "Palacio":
                indicador.transform.position = new Vector2(52.1f, 98.16f);
                break;
        }
    }

}
