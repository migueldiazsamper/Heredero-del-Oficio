using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minigame7Manager : MonoBehaviour
{
    [ SerializeField ] private Transform gameTransform;
    [ SerializeField ] private Transform piecePrefab;
    [ SerializeField ] private Transform backgroundPrefab; // Referencia al prefab del fondo
    [ SerializeField ] private Transform canvasTransform;

    [SerializeField] private Vector3 boardScale = Vector3.one;

    List< Transform > pieces;

    int emptyLocation;
    public int size = 3;

    int numberOfMoves = 0; 
    [ SerializeField ] TextMeshProUGUI movesText;

    void CreateGamePieces ( float gapThickness )
    {
        float width = 1 / ( float ) size;

        for ( int row = 0 ; row < size ; row++ )
        {
            for ( int col = 0 ; col < size ; col++ )
            {
                Transform piece = Instantiate( piecePrefab , gameTransform );
                pieces.Add( piece );
                piece.localPosition = new Vector3( -1 + ( 2 * width * col ) + width , 1 - ( 2 * width * row ) - width , 0 );
                piece.localScale = ( ( 2 * width ) - gapThickness ) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                bool isLast = row == size - 1 && col == size - 1;
                if ( isLast )
                {
                    emptyLocation = ( size * size ) - 1;
                    piece.gameObject.SetActive( false );
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent< MeshFilter >().mesh;
                    Vector2[] uv = new Vector2[ 4 ];

                    uv[ 0 ] = new Vector2( ( width * col ) + gap , 1 - ( ( width * ( row + 1 ) ) - gap ) );
                    uv[ 1 ] = new Vector2( ( width * ( col + 1 ) ) - gap , 1 - ( ( width * ( row + 1 ) ) - gap ) );
                    uv[ 2 ] = new Vector2( ( width * col ) + gap , 1 - ( ( width * row ) + gap ) );
                    uv[ 3 ] = new Vector2( ( width * ( col + 1 ) ) - gap , 1 - ( ( width * row ) + gap ) );

                    mesh.uv = uv;
                }
            }
        }

        // Genera el fondo del minijuego
        GenerateBackground();
    }

    void GenerateBackground()
    {
        // Instancia el fondo
        Transform background = Instantiate(backgroundPrefab, canvasTransform);

        // Establece la escala del fondo
        background.localScale = new Vector3(0.925f, 0.925f, 1);

        // Establece la posición del fondo, colocando un valor negativo en el eje Z para que esté detrás de las piezas
        background.localPosition = new Vector3(0, 0, 1);
    }


    void Start ()
    {
        numberOfMoves = 0;
        movesText.text = $"MOVIMIENTOS:\n{numberOfMoves}";
        pieces = new List<Transform>();
        CreateGamePieces(0.01f);
        Shuffle();
    }

    void Update ()
    {
        gameTransform.localScale = boardScale; // Aplica la escala del tablero
        movesText.text = $"MOVIMIENTOS:\n{numberOfMoves}";

        bool pieceClicked = Input.GetMouseButtonDown( 0 );
        if ( pieceClicked )
        {
            RaycastHit2D hit = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ) , Vector2.zero );

            if ( hit )
            {
                for ( int iterativePiece = 0 ; iterativePiece < pieces.Count ; iterativePiece++ )
                {
                    bool hitPiece = pieces[ iterativePiece ] == hit.transform;
                    if ( hitPiece )
                    {
                        if ( SwapIfValid( iterativePiece , -size , size ) )
                        {
                            numberOfMoves++;
                            break;
                        }
                        if ( SwapIfValid( iterativePiece , +size , size ) )
                        {
                            numberOfMoves++;
                            break;
                        }
                        if ( SwapIfValid( iterativePiece , -1 , 0 ) )
                        {
                            numberOfMoves++;
                            break;
                        }
                        if ( SwapIfValid( iterativePiece , +1 , size - 1 ) )
                        {
                            numberOfMoves++;
                            break;
                        }
                    }
                }
            }
        }

        if ( CheckCompletion() )
        {
            Debug.Log( "Game Completed!" );
        }
    }

    bool SwapIfValid ( int pieceIndex , int offset , int colCheck )
    {
        bool isValid = ( ( pieceIndex % size ) != colCheck ) && ( ( pieceIndex + offset ) == emptyLocation );
        if ( isValid )
        {
            ( pieces[ pieceIndex ] , pieces[ pieceIndex + offset ] ) = ( pieces[ pieceIndex + offset ] , pieces[ pieceIndex ] );
            ( pieces[ pieceIndex ].localPosition , pieces[ pieceIndex + offset ].localPosition ) = ( pieces[ pieceIndex + offset ].localPosition , pieces[ pieceIndex ].localPosition );
            emptyLocation = pieceIndex;

            return true;
        }

        return false;
    }

    bool CheckCompletion ()
    {
        for ( int pieceIndex = 0 ; pieceIndex < pieces.Count ; pieceIndex++ )
        {
            bool isIncorrect = pieces[ pieceIndex ].name != $"{pieceIndex}";
            if ( isIncorrect )
            {
                return false;
            }
        }

        return true;
    }

    void Shuffle ()
    {
        int count = 0;
        int last = 0;

        while ( count < ( size * size * size ) )
        {
            int randomLocation = Random.Range( 0 , size * size );

            bool lastMoveForbidden = randomLocation == last;
            if ( lastMoveForbidden )
            {
                continue;
            }

            last = emptyLocation;

            if ( SwapIfValid( randomLocation , -size , size ) )
            {
                count++;
            }
            else if ( SwapIfValid( randomLocation , size , size ) )
            {
                count++;
            }
            else if ( SwapIfValid( randomLocation , -1 , 0 ) )
            {
                count++;
            }
            else if ( SwapIfValid( randomLocation , +1 , size - 1 ) )
            {
                count++;
            }
        }
    }
}
