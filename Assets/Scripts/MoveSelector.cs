using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;
    private List<Vector2Int> availableMoves;
    private List<GameObject> moveHighlights;
    private List<GameObject> attackHighlights;
    private int numOfAttackTiles;


    // Use this for initialization
    void Start ()
    {
        this.enabled = false;
        numOfAttackTiles = 0;
        availableMoves = new List<Vector2Int>();
        
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);

        moveHighlights = new List<GameObject>();
        for (int i = 0; i < 27; i++)
        {
            Debug.Log("for index: " + i);
            moveHighlights.Add(Instantiate(moveLocationPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform));
            moveHighlights[i].SetActive(false);
        }

        attackHighlights = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            attackHighlights.Add(Instantiate(attackLocationPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform));
            attackHighlights[i].SetActive(false);
        }
        //Debug.Log("START HAS FINISHED");
    }
	
    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        availableMoves = GameManager.instance.getAllowedMoveList(movingPiece);
        activateMoveTiles();
        this.enabled = true;
    }

    private void ExitState()
    {
        this.enabled = false;
        numOfAttackTiles = 0;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        availableMoves.Clear();
        TileSelector selector = GetComponent<TileSelector>();
        selector.EnterState();
    }

	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            //Debug.Log("before mouse down");
            if (Input.GetMouseButtonDown(0))
            {
                //if user grid choice is a valid one, then move there
                if (availableMoves.Contains(gridPoint) && GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    //check if pawn has moved
                    Piece currentPiece = movingPiece.GetComponent(typeof(Piece)) as Piece;
                    if (currentPiece.type == PieceType.Pawn)
                    {
                        Pawn movedPawn = movingPiece.GetComponent(typeof(Pawn)) as Pawn;
                        movedPawn.hasMoved = true;
                    }
                    deactivateMoveTiles();
                    GameManager.instance.Move(movingPiece, gridPoint);
                    ExitState();
                } 
                else if(GameManager.instance.GridForPiece(movingPiece) == gridPoint)
                {
                    //else if entered if user deselects piece
                    deactivateMoveTiles();
                    ExitState();
                }
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
	}


    //sets active to tiles which are part of the available moves
    private void activateMoveTiles()
    {
        int numOfMoves = availableMoves.Count;
        int numMove = 0;

        while(numOfAttackTiles + numMove < numOfMoves)
        {
            Vector2Int specificGrid = availableMoves[numOfAttackTiles + numMove];
            if (GameManager.instance.DoesPieceOnGridBelongToCurrent(specificGrid))
            {
                attackHighlights[numOfAttackTiles].transform.position = Geometry.PointFromGrid(specificGrid);
                attackHighlights[numOfAttackTiles].SetActive(true);
                numOfAttackTiles++;
            }
            else
            {
                moveHighlights[numMove].transform.position = Geometry.PointFromGrid(specificGrid);
                moveHighlights[numMove].SetActive(true);
                numMove++;
            }
        }
    }

    //deactivates move tiles previously activated by available moves list
    private void deactivateMoveTiles()
    {
        int numOfMoves = availableMoves.Count;
        for (int i = 0; i < numOfMoves - numOfAttackTiles; i++)
        {
                moveHighlights[i].SetActive(false);     
        }

        for (int i = 0; i < numOfAttackTiles; i++)
        {
            attackHighlights[i].SetActive(false);
        }
    }
}
