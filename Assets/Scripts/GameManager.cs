using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    [SerializeField]
    private GameObject[,] pieces;

    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        pieces = new GameObject[8, 8];

        white = new Player("white", true);
        black = new Player("black", false);

        currentPlayer = white;
        otherPlayer = black;

        InitialSetup();
    }

    private void InitialSetup()
    {
        /* UNCOMMENT TO HAVE ALL PIECES SPAWN NORMALLY!!!!
        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 0, 7);
        AddPiece(blackKnight, black, 1, 7);
        AddPiece(blackBishop, black, 2, 7);
        AddPiece(blackQueen, black, 3, 7);
        AddPiece(blackKing, black, 4, 7);
        AddPiece(blackBishop, black, 5, 7);
        AddPiece(blackKnight, black, 6, 7);
        AddPiece(blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(blackPawn, black, i, 6);
        } */

        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 3, 3);
        AddPiece(blackKnight, black, 0, 6);
    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        if (selectedPiece)
        {
            board.SelectPiece(selectedPiece);
        }
    }

    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null) {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    //moves piece to specified grid coordinate
    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, gridPoint);
    }

    //Returns a list of allowed moves for the passed piece
    public List<Vector2Int> getAllowedMoveList(GameObject movingPiece)
    {
        Piece currentPiece = movingPiece.GetComponent(typeof(Piece)) as Piece;
        Vector2Int gridCurrentPiece = GridForPiece(movingPiece);

        List<Vector2Int> possibleMoves = currentPiece.MoveLocations(gridCurrentPiece);

        //remove all coordinates from possible moves that are blocked by pieces
        removeBlockedGrids(currentPiece, possibleMoves, gridCurrentPiece);

        return possibleMoves;
    }


    //remove possible locations past the pieces that are blocking the currently selected piece.
    public void removeBlockedGrids(Piece currentPiece, List<Vector2Int> possibleMoves, Vector2Int gridCurrentPiece)
    {
        //obtain a list of all pieces on field
        List<GameObject> currentPiecesOnField = currentPlayer.pieces;
        currentPiecesOnField.AddRange(otherPlayer.pieces);

        int numOfOtherPieces = currentPiecesOnField.Count;
        Vector2Int gridOtherPiece;

        if (currentPiece.type != PieceType.King || currentPiece.type != PieceType.Knight || currentPiece.type != PieceType.Pawn)
            for (int i = 0; i < numOfOtherPieces; i++)
            {
                gridOtherPiece = GridForPiece(currentPiecesOnField[i]);
                
                if (possibleMoves.Contains(gridOtherPiece))
                {
                    if (currentPiece.type == PieceType.Rook) checkRookMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);

                    if (currentPiece.type == PieceType.Bishop) checkBishopMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);
                    if (currentPiece.type == PieceType.Queen)
                    {
                        checkBishopMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);
                        checkRookMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);
                    }
                }
            }

        //remove all coordinates that are out of bounds
        possibleMoves.RemoveAll(move => move.x > 7 || move.x < 0 || move.y > 7 || move.y < 0);
    }

    public void checkRookMoves(List<Vector2Int> moveList, Vector2Int gridOtherPiece, Vector2Int gridCurrentPiece)
    {

        moveList.RemoveAll(move => gridOtherPiece.x < gridCurrentPiece.x && gridOtherPiece.y == gridCurrentPiece.y && move.x < gridOtherPiece.x && move.y == gridOtherPiece.y ||
                                   gridOtherPiece.x > gridCurrentPiece.x && gridOtherPiece.y == gridCurrentPiece.y && move.x > gridOtherPiece.x && move.y == gridOtherPiece.y ||
                                   gridOtherPiece.y < gridCurrentPiece.y && gridOtherPiece.x == gridCurrentPiece.x && move.y < gridOtherPiece.y && move.x == gridOtherPiece.x ||
                                   gridOtherPiece.y > gridCurrentPiece.y && gridOtherPiece.x == gridCurrentPiece.x && move.y > gridOtherPiece.y && move.x == gridOtherPiece.x);
    }
    
    public void checkBishopMoves(List<Vector2Int> moveList, Vector2Int gridOtherPiece, Vector2Int gridCurrentPiece)
    {
        moveList.RemoveAll(move => gridOtherPiece.x < gridCurrentPiece.x && gridOtherPiece.y > gridCurrentPiece.y && move.x < gridOtherPiece.x && move.y > gridOtherPiece.y ||
                                   gridOtherPiece.x > gridCurrentPiece.x && gridOtherPiece.y > gridCurrentPiece.y && move.x > gridOtherPiece.x && move.y > gridOtherPiece.y ||
                                   gridOtherPiece.x < gridCurrentPiece.x && gridOtherPiece.y < gridCurrentPiece.y && move.x < gridOtherPiece.x && move.y < gridOtherPiece.y ||
                                   gridOtherPiece.x > gridCurrentPiece.x && gridOtherPiece.y < gridCurrentPiece.y && move.x > gridOtherPiece.x && move.y < gridOtherPiece.y);
    }
    

}
