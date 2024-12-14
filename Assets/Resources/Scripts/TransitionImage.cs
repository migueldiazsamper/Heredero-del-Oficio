using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionImage : MonoBehaviour
{

    public static TransitionImage instance;
    private Animator animator;
    [SerializeField] private GameObject transitionImage;


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
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        StartCoroutine(LoadAnimationAfterTransition());
    }
    private IEnumerator LoadAnimationAfterTransition()
    {
        transitionImage.GetComponent<Animator>().SetBool("AnimateOut", true);
        yield return new WaitForSeconds( 1f );

    }

    public void LoadAnimationBeforeTransition(){
        
    }
}
