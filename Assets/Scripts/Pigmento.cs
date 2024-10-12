using UnityEngine;
using UnityEngine.EventSystems;

// Clase que representa un pigmento
public class Pigmento : MonoBehaviour, IPointerClickHandler
{
    // Identificador del color del pigmento
    public string color;

    // Referencia al PigmentosManager
    private PigmentosManager manager;

    private void Start()
    {
        // Obtiene la referencia al PigmentosManager
        manager = FindObjectOfType<PigmentosManager>();
    }

    // Método que se llama al hacer clic en el pigmento
    public void OnPointerClick(PointerEventData eventData)
    {
        // Intenta colocar el pigmento en un slot
        manager.SelectPigment(this);
    }
}