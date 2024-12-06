using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScaler : MonoBehaviour
{
    public static ButtonsScaler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Cancelar la suscripción al evento
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetButtonsScaleToOne();
        SubscribeToButtonClicks();
    }

    private void SetButtonsScaleToOne()
    {
        // Recolectar todos los botones, incluyendo los desactivados
        List<Button> buttons = new List<Button>();
        foreach (GameObject rootObject in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            buttons.AddRange(rootObject.GetComponentsInChildren<Button>(true)); // `true` incluye desactivados
        }

        // Ajustar la escala de cada botón
        foreach (Button button in buttons)
        {
            button.transform.localScale = Vector3.one;
        }

        Debug.Log($"Scaled {buttons.Count} buttons to 1 in scene: {SceneManager.GetActiveScene().name}");
    }

    private void SubscribeToButtonClicks()
    {
        // Recolectar todos los botones nuevamente
        List<Button> buttons = new List<Button>();
        foreach (GameObject rootObject in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            buttons.AddRange(rootObject.GetComponentsInChildren<Button>(true)); // `true` incluye desactivados
        }

        // Suscribir al evento de clic
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners(); // Limpiar listeners previos
            button.onClick.AddListener(() =>
            {
                ResetButtonScale(button);
            });
        }

        Debug.Log($"Subscribed to click events for {buttons.Count} buttons in scene: {SceneManager.GetActiveScene().name}");
    }

    private void ResetButtonScale(Button button)
    {
        button.transform.localScale = Vector3.one;
        Debug.Log($"Button {button.name} clicked and scale reset to 1");
    }
}
