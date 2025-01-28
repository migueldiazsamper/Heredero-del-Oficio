using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Definición de la clase UpdatePercentage que hereda de MonoBehaviour
public class UpdatePercentage : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI para mostrar el porcentaje
    [ SerializeField ] TextMeshProUGUI percentageText;
    [ SerializeField ] GameObject bienHecho;
    [ SerializeField ] GameObject letras;
    // Variable estática pública para almacenar el porcentaje del total
    static public int percentageOfTotal = 0;
    private bool isDone = false;

    void Start()
    {
        bienHecho.SetActive( false );
        isDone = false;
    }

    // Método Update que se llama una vez por frame
    void Update ()
    {
        // Actualiza el texto del porcentaje en pantalla
        percentageText.text = percentageOfTotal.ToString() + "%";

        if ( percentageOfTotal == 100 && !isDone )
        {
            isDone = true;
            //ShowWellDone();
        }
    }

    private void ShowWellDone()
    {
        if (isDone)
        {
            StartCoroutine(IncreaseScale(letras));
        }
    }

    private IEnumerator IncreaseScale ( GameObject letras )
    {
        yield return new WaitForSeconds(0.5f); // Pequeña espera inicial

        // Aseguramos que bienHecho comience con alpha 0
        CanvasGroup canvasGroup = bienHecho.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = bienHecho.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        bienHecho.SetActive(true);

        // Incremento progresivo del alfa hasta el máximo
        float fadeInSpeed = 2.0f; // Velocidad de aumento de alfa
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1.0f, fadeInSpeed * Time.deltaTime);
            if (canvasGroup.alpha > 0.99f) canvasGroup.alpha = 1.0f; // Asegura el valor máximo
            yield return null;
        }

        canvasGroup.alpha = 1.0f; // Asegura que el alfa sea exactamente 1

        // Escalado progresivo hasta alcanzar la escala mayor que 7
        Vector3 targetScale = new Vector3(7.0f, 7.0f, 7.0f);
        float scaleSpeed = 2.0f; // Velocidad de escalado

        while (Vector3.Distance(letras.transform.localScale, targetScale) > 0.01f)
        {
            letras.transform.localScale = Vector3.Lerp(letras.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }

        letras.transform.localScale = targetScale; // Asegura que la escala sea exactamente 7

        // Espera un poco antes de iniciar la disminución del alfa
        yield return new WaitForSeconds(0.5f);

        // Disminución progresiva del alfa
        float fadeOutSpeed = 5000.0f; // Velocidad de desvanecimiento
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, fadeOutSpeed * Time.deltaTime);
            if (canvasGroup.alpha < 0.01f) canvasGroup.alpha = 0; // Evita valores cercanos a 0
            yield return null;
        }

        canvasGroup.alpha = 0; // Asegura que el alfa sea exactamente 0

        // Desactiva el objeto bienHecho
        bienHecho.SetActive(false);
    }
}