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
    

	// Use this for initialization
	void Start ()
    {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
	}
	
    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;
    }

    private void ExitState()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
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
                List<Vector2Int> availableMoves = GameManager.instance.getAllowedMoveList(movingPiece);

                Debug.Log("after mouse down");
                //Ref point 2: check for valid move location
                if (availableMoves.Contains(gridPoint) && GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    
                    //check if pawn has moved
                    Piece currentPiece = movingPiece.GetComponent(typeof(Piece)) as Piece;
                    if (currentPiece.type == PieceType.Pawn)
                    {
                        Pawn movedPawn = movingPiece.GetComponent(typeof(Pawn)) as Pawn;
                        movedPawn.hasMoved = true;
                    }

                    GameManager.instance.Move(movingPiece, gridPoint);
                    Debug.Log("Exited move selector");
                    ExitState();
                } 
                else if(GameManager.instance.GridForPiece(movingPiece) == gridPoint)
                {
                    Debug.Log("Deselect Piece");
                    ExitState();
                }
                Debug.Log("exiting mouse down if statement");
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
	}
}
