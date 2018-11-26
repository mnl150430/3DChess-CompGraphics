using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        
        locations.Add(new Vector2Int(gridPoint.x - 1, gridPoint.y - 1));
        locations.Add(new Vector2Int(gridPoint.x + 1, gridPoint.y - 1));
        locations.Add(new Vector2Int(gridPoint.x - 1, gridPoint.y + 1));
        locations.Add(new Vector2Int(gridPoint.x + 1, gridPoint.y + 1));

        locations.Add(new Vector2Int(gridPoint.x, gridPoint.y - 1));
        locations.Add(new Vector2Int(gridPoint.x, gridPoint.y + 1));
        locations.Add(new Vector2Int(gridPoint.x + 1, gridPoint.y));
        locations.Add(new Vector2Int(gridPoint.x - 1, gridPoint.y));
        
        return locations;
    }
}
