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
            SetAlpha( 1f );
        }
        else if ( PhasesManager.instance.currentPhase == 12 && PhasesManager.instance.vecesMina == 5 ) // saco pigmento
        {
            slot.GetComponent<Image>().sprite = sacoPigmento;
            SetAlpha( 1f );
        }
        else
        {
            slot.GetComponent<Image>().sprite = null;
            SetAlpha( 0f );
        }
    }

    private void SetAlpha ( float alpha )
    {
        Color color = slot.GetComponent<Image>().color;
        color.a = alpha;
        slot.GetComponent<Image>().color = color;
    }
}
