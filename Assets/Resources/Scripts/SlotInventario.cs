using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotInventario : MonoBehaviour
{
    [ SerializeField ] private Sprite sacoArena;
    [ SerializeField ] private Sprite sacoPigmento;
    [ SerializeField ] private GameObject slot;

    private void Update ()
    {
        if ( PhasesManager.instance.currentPhase == 2 && PhasesManager.instance.vecesMina >= 2 ) // saco arena
        {
            slot.GetComponent<Image>().sprite = sacoArena;
        }
        else if ( PhasesManager.instance.currentPhase == 12 && PhasesManager.instance.vecesMina == 5 ) // saco pigmento
        {
            slot.GetComponent<Image>().sprite = sacoPigmento;
        }
        else
        {
            slot.GetComponent<Image>().sprite = null;
        }
    }
}
