using System.Collections.Generic;
using UnityEngine;

// Clase que gestiona la lógica de los pigmentos y los slots
public class PigmentosManager : MonoBehaviour
{
    // Lista de pigmentos
    public List<Pigmento> pigmentos;

    // Referencia a los dos slots
    public SlotPigmento slot1;
    public SlotPigmento slot2;

    // Referencia al sprite que representa el color mezclado
    public SpriteRenderer mixedColorSprite;

    // Diccionario que define las mezclas de colores
    private Dictionary<string, Color> colorMixes = new Dictionary<string, Color>
    {
        { "CyanMagenta", Color.blue }, // Cian + Magenta = Azul
        { "MagentaCyan", Color.blue }, // Magenta + Cian = Azul (inversa)
        { "CyanYellow", Color.green }, // Cian + Amarillo = Verde
        { "YellowCyan", Color.green }, // Amarillo + Cian = Verde (inversa)
        { "MagentaYellow", Color.red }, // Magenta + Amarillo = Rojo
        { "YellowMagenta", Color.red }, // Amarillo + Magenta = Rojo (inversa)
        { "CyanBlack", new Color(0f, 0.5f, 0.5f) }, // Cian + Negro = Cian Oscuro
        { "BlackCyan", new Color(0f, 0.5f, 0.5f) }, // Negro + Cian = Cian Oscuro (inversa)
        { "MagentaBlack", new Color(0.5f, 0f, 0.5f) }, // Magenta + Negro = Magenta Oscuro
        { "BlackMagenta", new Color(0.5f, 0f, 0.5f) }, // Negro + Magenta = Magenta Oscuro (inversa)
        { "YellowBlack", new Color(0.5f, 0.5f, 0f) }, // Amarillo + Negro = Amarillo Oscuro
        { "BlackYellow", new Color(0.5f, 0.5f, 0f) }, // Negro + Amarillo = Amarillo Oscuro (inversa)
        { "CyanWhite", new Color(0.5f, 1f, 1f) }, // Cian + Blanco = Cian Claro
        { "WhiteCyan", new Color(0.5f, 1f, 1f) }, // Blanco + Cian = Cian Claro (inversa)
        { "MagentaWhite", new Color(1f, 0.5f, 1f) }, // Magenta + Blanco = Magenta Claro
        { "WhiteMagenta", new Color(1f, 0.5f, 1f) }, // Blanco + Magenta = Magenta Claro (inversa)
        { "YellowWhite", new Color(1f, 1f, 0.5f) }, // Amarillo + Blanco = Amarillo Claro
        { "WhiteYellow", new Color(1f, 1f, 0.5f) }, // Blanco + Amarillo = Amarillo Claro (inversa)
        { "BlackWhite", Color.gray }, // Negro + Blanco = Gris
        { "WhiteBlack", Color.gray }, // Blanco + Negro = Gris (inversa)
        // Agrega más combinaciones según sea necesario
    };

    // Método que se llama al seleccionar un pigmento
    public void SelectPigment(Pigmento pigmento)
    {
        // Verifica si ambos slots están ocupados
        if (!slot1.IsEmpty() && !slot2.IsEmpty())
        {
            Debug.Log("Ambos slots están ocupados. No se puede seleccionar más colores.");
            return;
        }

        // Coloca el pigmento en el primer slot vacío
        if (slot1.IsEmpty())
        {
            slot1.PlacePigment(pigmento);
        }
        else if (slot2.IsEmpty())
        {
            slot2.PlacePigment(pigmento);
        }
    }

    // Método que se llama al usar la herramienta en el trigger
    public void MixColors()
    {
        // Verifica si ambos slots están ocupados
        if (slot1.IsEmpty() || slot2.IsEmpty())
        {
            Debug.Log("Ambos slots deben estar ocupados para mezclar colores.");
            return;
        }

        // Obtiene los colores de los pigmentos en los slots
        string color1 = slot1.currentPigment.color;
        string color2 = slot2.currentPigment.color;

        // Genera la clave para buscar en el diccionario de mezclas
        string mixKey = color1 + color2;

        // Verifica si la mezcla existe en el diccionario
        if (colorMixes.ContainsKey(mixKey))
        {
            // Cambia el color del sprite al color mezclado
            mixedColorSprite.color = colorMixes[mixKey];
        }
        else
        {
            Debug.Log("Mezcla de colores no definida.");
        }
    }
}