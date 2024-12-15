using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionImage : MonoBehaviour
{

    public static TransitionImage instance;
    private Animator animator;
    
    private GameObject transitionImage;


    /// <summary>
    /// Método Awake: se ejecuta antes de Start. Configura la instancia Singleton
    /// y evita que se destruya al cambiar de escena.
    /// </summary>

    private void Awake ()
    {
        // Comprueba si ya existe una instancia de TransitionImage.
        bool noExisteInstancia = instance == null;

        if ( noExisteInstancia )
        {
            instance = this;

            // Hace que este GameObject persista entre escenas.
            DontDestroyOnLoad( gameObject );
        }
        else
        {
            // Si ya hay una instancia, destruye este GameObject duplicado.
            Destroy( gameObject );
        }
        // Suscripción al evento de carga de escena
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        transitionImage = GameObject.Find("TransitionImage");
        StartCoroutine(LoadAnimationAfterTransition());
    }


    private IEnumerator LoadAnimationAfterTransition()
    {
        transitionImage.GetComponent<Animator>().Play("TransitionImage_Out");
        yield return new WaitForSeconds( 1f );
        transitionImage.SetActive(false);

    }

    public void OnSceneChange(){
        Debug.Log("Call from OnSceneChange Correct");
        StartCoroutine(LoadAnimationBeforeTransition());
    }

    
    private IEnumerator LoadAnimationBeforeTransition()
    {
        Debug.Log("Call from TransitionIn Coroutine Correct");
        transitionImage.SetActive(true);
        transitionImage.GetComponent<Animator>().Play("TransitionImage_In");
        yield return new WaitForSeconds( 0f );

    }
}
