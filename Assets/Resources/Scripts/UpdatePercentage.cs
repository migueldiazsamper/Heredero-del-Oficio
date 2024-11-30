using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Definición de la clase UpdatePercentage que hereda de MonoBehaviour
public class UpdatePercentage : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI para mostrar el porcentaje
    [ SerializeField ] TextMeshProUGUI percentageText;
    // Variable estática pública para almacenar el porcentaje del total
    static public int percentageOfTotal = 0;

    // Método Update que se llama una vez por frame
    void Update ()
    {
        // Actualiza el texto del porcentaje en pantalla
        percentageText.text = percentageOfTotal.ToString() + "%";
    }
}