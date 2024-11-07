using UnityEngine;
using UnityEngine.EventSystems;

// Clase que representa un pigmento
public class Pigmento : MonoBehaviour
{
    // Identificador del color del pigmento
    public string colorString {get; private set;}

    public Color ProvideColor(){
        switch (colorString){
            case "Black":   return Color.black;
            case "Magenta": return Color.magenta;
            case "Cyan":    return Color.cyan;
            case "Yellow":  return Color.yellow;
            case "White":   return Color.white;
            default: return Color.white;
        }
    }

}