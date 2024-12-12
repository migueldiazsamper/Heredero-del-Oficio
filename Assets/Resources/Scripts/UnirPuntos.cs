using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class UnirPuntos : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int IDout; //Con quién quieres conectar
    [SerializeField] private int IDin; //Quién eres
    private bool isDragging; //Booleano que indica que la acción de arrastre está comenzada
    private Vector3 endPoint; //Punto final de la línea, se usa para comprobar si la línea llega hasta otro punto o no
    private bool isConnected = false;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; //Nº de puntos de la línea (siempre será 2)
    }

    private void Update()
    {
        if(!isConnected){
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); //Creamos un raycast hacia el punto sobre el que se ha hecho click
                if (hit.collider != null && hit.collider.gameObject == gameObject) 
                {
                    // Reproduce el sonido de clicar un punto
                    AudioManager.GetInstance().Play(AudioManager.GetInstance().grabPoint);

                    isDragging = true;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0f;
                    lineRenderer.SetPosition(0,mousePosition); //Creamos el primer punto de la línea donde está el ratón
                }
            }
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Actualizamos la posición del ratón
                mousePosition.z = 0f;                                                           //para asegurarnos que está en el plano correcto
                lineRenderer.SetPosition(1,mousePosition); //Actualizamos el segundo punto de la línea, la línea sigue al ratón en tiempo real
                endPoint = mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                RaycastHit2D hit = Physics2D.Raycast(endPoint, Vector2.zero);
                if (hit.collider != null && hit.collider.TryGetComponent(out UnirPuntos unirPuntos) && IDout == unirPuntos.GetIDin())  //Chequeamos que el ID del punto que comienza
                                                                                                                            //la línea coincide con el que recibe
                {
                    // Reproduce el sonido de unir un punto
                    AudioManager.GetInstance().Play(AudioManager.GetInstance().linkPoint);

                    Debug.Log("UNIÓN");
                    isConnected = true;
                }
                else    //Si se suelta la línea sin que esté en ningún punto correcto, se borra la línea
                {
                    lineRenderer.positionCount = 0;
                }

                lineRenderer.positionCount = 2; //Aseguramos que la línea sigue teniendo los parámetros correctos
            }
        }
    }

    public int GetIDin()
    {
        return IDin;
    }
    
}