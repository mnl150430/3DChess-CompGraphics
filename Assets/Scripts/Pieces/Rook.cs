using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    private int greaterBounds = 8;
    private int lesserBounds = -1;
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        Vector2Int possibleLocation;
        for (int i=0; i < 4;i++)
        {
            possibleLocation = gridPoint;
            while(true)
            {
                possibleLocation = RookDirections[i] + possibleLocation;

                //Debug.Log("rook-X: " + possibleLocation.x + " rook-Y: " + possibleLocation.y);
                if (possibleLocation.x >= greaterBounds || possibleLocation.y >= greaterBounds || possibleLocation.x <= lesserBounds || possibleLocation.y <= lesserBounds)
                {
                    break;
                }  
                else
                {
                    locations.Add(possibleLocation);
                }
            }
        }
        return locations;
    }
}
