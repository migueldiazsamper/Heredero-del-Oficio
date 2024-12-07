using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlighted : MonoBehaviour
{
    public static ButtonHighlighted Instance { get; private set; }

    private List<Button> allButtons = new List<Button>();

    void Awake()
    {
        // Implementación del patrón singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Suscripción al evento de carga de escena
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se ejecuta cada vez que se carga una escena
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        CollectAllButtons();
    }

    // Recolectar todos los botones de la escena
    void CollectAllButtons()
    {
        allButtons.Clear();
        allButtons.AddRange(FindObjectsOfType<Button>(true)); // true para incluir objetos inactivos

        foreach (var button in allButtons)
        {
            AddEventTriggers(button);
        }

        Debug.Log($"Se han recolectado {allButtons.Count} botones en la escena '{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}'.");
    }

    // Añadir eventos de hover a cada botón
    void AddEventTriggers(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // Crear o encontrar la lista de entradas
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        // Evento de PointerEnter (hover)
        var pointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnter.callback.AddListener((eventData) => OnHoverEnter(button));

        // Evento de PointerExit (salir del hover)
        var pointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExit.callback.AddListener((eventData) => OnHoverExit(button));

        // Añadir los eventos al trigger
        trigger.triggers.Add(pointerEnter);
        trigger.triggers.Add(pointerExit);
    }

    void OnHoverEnter(Button button)
    {
        Debug.Log($"Hover sobre el botón: {button.name}");
    }

    void OnHoverExit(Button button)
    {
        Debug.Log($"Se dejó de hacer hover sobre el botón: {button.name}");
    }
}
