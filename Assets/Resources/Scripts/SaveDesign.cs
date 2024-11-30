using UnityEngine;

// Definici�n de la clase SaveDesign que hereda de MonoBehaviour
public class SaveDesign : MonoBehaviour
{
    // Lista de GameObjects (Sprites) de los diferentes dise�os
    public GameObject[] sprites;

    // Nueva posici�n para el sprite seleccionado
    private Vector2 newPosition = new Vector2( 8, 3 );
    private GameObject selectedSprite;

    // M�todo Start que se llama al iniciar el script
    void Start ()
    {
        // Activa todos los sprites al iniciar
        foreach ( GameObject sprite in sprites )
        {
            sprite.SetActive( true );
        }
    }

    // M�todo Update que se llama una vez por frame
    void Update ()
    {
        // Detecta los clicks del rat�n
        bool mouseIsClicked = Input.GetMouseButtonDown( 0 );

        if ( mouseIsClicked )
        {
            // Convierte la posici�n del rat�n a coordenadas del mundo
            Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            RaycastHit2D hit = Physics2D.Raycast( mousePos, Vector2.zero );

            // Verifica si se ha hecho clic en un sprite
            bool spriteIsClicked = hit.collider != null;

            if ( spriteIsClicked )
            {
                // Recorre la lista de sprites para guardar el que ha sido seleccionado
                foreach ( GameObject sprite in sprites ) 
                {
                    bool spriteIsSpriteSelected = hit.collider.gameObject == sprite;

                    if ( spriteIsSpriteSelected )
                    {
                        selectedSprite = sprite;
                        break;
                    }
                }

                // Si se seleccion� un sprite, desactiva los dem�s y reposiciona el seleccionado
                bool spriteIsSelected = selectedSprite != null;

                if ( spriteIsSelected )
                {
                    foreach ( GameObject sprite in sprites )
                    {
                        bool spriteIsNotSelected = sprite != selectedSprite;

                        if ( spriteIsNotSelected )
                        {
                            sprite.SetActive( false );
                        }
                    }

                    // Mueve y escala el sprite seleccionado
                    selectedSprite.transform.position = newPosition;
                    selectedSprite.transform.localScale = new Vector2( 0.3f,0.3f );
                }
            }
        }
    }
}
