﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{

    public GameObject tileHighlightPrefab;
    private GameObject tileHighlight;

    

    // Use this for initialization
    void Start ()
    {
        Vector2Int gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);

        
    }

    public void EnterState()
    {
        enabled = true;
    }


    private void ExitState(GameObject movingPiece)
    {
        
        this.enabled = false;
        tileHighlight.SetActive(false);
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }

	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            Geometry.PointFromGrid(gridPoint);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pointer at space (" + gridPoint.x + ", " + gridPoint.y + ")");
                GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);

                if(GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece) && GameManager.instance.safeToMove == true)
                {
                    //changes piece material to selected
                    GameManager.instance.SelectPiece(selectedPiece);

                    //ExitState goes here
                    ExitState(selectedPiece);
                }
            }
        }

        else
        {
            tileHighlight.SetActive(false);
        }
        
        
	}
}
