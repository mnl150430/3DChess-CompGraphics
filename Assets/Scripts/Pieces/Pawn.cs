

using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public bool hasMoved = false;
   
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
    
        if(color == PieceColor.White)
        {
            locations.Add(RookDirections[0] + gridPoint);
            locations.Add(BishopDirections[3] + gridPoint);
            locations.Add(BishopDirections[0] + gridPoint);

            if(hasMoved == false) locations.Add(RookDirections[0] * 2 + gridPoint);
        }
        else
        {
            locations.Add(RookDirections[2] + gridPoint);
            locations.Add(BishopDirections[1] + gridPoint);
            locations.Add(BishopDirections[2] + gridPoint);

            if (hasMoved == false) locations.Add(RookDirections[2] * 2 + gridPoint);
        }
       

        return locations;
    }
}
