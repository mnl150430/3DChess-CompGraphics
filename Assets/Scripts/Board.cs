using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectedMaterial;
    public float speed;
    public const float maxHeight = 1f;
    private GameObject tempPiece;
    private Vector3 finalLocation;
    private Vector3 intermediateLocation;
    private enum Direction {UP, DOWN, HORIZONTAL, NONE};
    Direction moveDirection = Direction.NONE;
    private float startTime;

    void Start()
    {
        speed = .4f;
    }

        void Update()
    {
        if (moveDirection == Direction.UP)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / Vector3.Distance(tempPiece.transform.position, intermediateLocation); ;
            tempPiece.transform.position = Vector3.Lerp(tempPiece.transform.position, intermediateLocation, fracJourney);
            //Debug.Log("inbetween pos: " + tempPiece.transform.position + " Expected post: " + intermediateLocation);
            
            if (tempPiece.transform.position == intermediateLocation)
            {
                //Debug.Log("entered first if");
                startTime = Time.time;
                intermediateLocation = finalLocation;
                intermediateLocation.y = intermediateLocation.y + maxHeight;
                moveDirection = Direction.HORIZONTAL;
            }
            
        }
        else if (moveDirection == Direction.HORIZONTAL)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / Vector3.Distance(tempPiece.transform.position, intermediateLocation); ;
            tempPiece.transform.position = Vector3.Lerp(tempPiece.transform.position, intermediateLocation, fracJourney);
            //Debug.Log("HORIZONTAL pos: " + tempPiece.transform.position);
            if (tempPiece.transform.position == intermediateLocation)
            {
                //Debug.Log("entered first if 2");
                startTime = Time.time;
                moveDirection = Direction.DOWN;
            }
        }
        else if (moveDirection == Direction.DOWN)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / Vector3.Distance(tempPiece.transform.position, finalLocation); ;
            tempPiece.transform.position = Vector3.Lerp(tempPiece.transform.position, finalLocation, fracJourney);
            //Debug.Log("DOWN pos: " + tempPiece.transform.position);
            if (tempPiece.transform.position == finalLocation)
            {
                moveDirection = Direction.NONE;
            }
        }
    }

    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint),
            Quaternion.identity, gameObject.transform);
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public IEnumerator MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        tempPiece = piece;
        finalLocation = Geometry.PointFromGrid(gridPoint);
        //Debug.Log("initial pos: " + piece.transform.position);
    
        intermediateLocation = piece.transform.position;
        intermediateLocation.y = intermediateLocation.y + maxHeight;
       // Debug.Log("maxHeight pos: " + intermediateLocation);
        
        //Debug.Log("DONE");
        yield return StartCoroutine("CheckMoveDirection");
    }

    IEnumerator CheckMoveDirection()
    {
        startTime = Time.time;
        moveDirection = Direction.UP;
        while (moveDirection != Direction.NONE)
        {
            yield return null; // wait until next frame
        }
        Debug.Log("COROUTINE FINISHED");
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = defaultMaterial;
    }
}
