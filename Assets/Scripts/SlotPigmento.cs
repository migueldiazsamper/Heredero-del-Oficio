using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPigmento : MonoBehaviour
{
    // Referencia al pigmento que ocupa el slot
    public Pigmento currentPigment;

    // Verifica si el slot está vacío
    public bool IsEmpty ()
    {
        return currentPigment == null;
    }

    // Coloca un pigmento en el slot
    public void PlacePigment( Pigmento pigmento )
    {
        currentPigment = pigmento;
        pigmento.transform.position = transform.position;
    }
}
