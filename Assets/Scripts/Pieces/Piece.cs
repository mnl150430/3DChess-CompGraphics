using System.Collections.Generic;
using UnityEngine;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};
public enum PieceColor { White, Black}

public abstract class Piece : MonoBehaviour
{

    public PieceType type;
    public PieceColor color;

    //forward, right, backward, left
    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0)};

    //top right, bottom right, bottom left, top left
    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);
}
