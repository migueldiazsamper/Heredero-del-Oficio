using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void SetPlayerSpawnPoint()
    {
        if ( PhasesManager.instance.currentPhase == 1 )
        {
            GameObject.Find("PersonajePrincipal").transform.position = PhasesManager.instance.zonasDeAparicion[0]; // Casa
        }
        else if ( PhasesManager.instance.currentPhase == 2 && PhasesManager.instance.vecesMina == 3 )
        {
            GameObject.Find("PersonajePrincipal").transform.position = PhasesManager.instance.zonasDeAparicion[1]; // Mina
        }
        else if ( PhasesManager.instance.currentPhase == 10 || PhasesManager.instance.currentPhase == 16 )
        {
            GameObject.Find("PersonajePrincipal").transform.position = PhasesManager.instance.zonasDeAparicion[3]; // Hornos
        }
        else
        {
            GameObject.Find("PersonajePrincipal").transform.position = PhasesManager.instance.zonasDeAparicion[2]; // Fábrica
        }
    }

    private void Start()
    {
        SetPlayerSpawnPoint();
    }
}
