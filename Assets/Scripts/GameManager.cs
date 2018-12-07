﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
    public GameObject[,] pieces;
    public List<GameObject> allPieces;

    public SavedData saver;

    public bool safeToMove;
    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    public String playerTurn;
    public Text Player1;
    public Text Player2;

    public GameObject winPanel;
    public GameObject GameoverTxt;
    public Text WinPlayerName;

    public Button Player1View;
    public Button Player2View;

    public AudioSource playerMoved;
    
    void Awake()
    {
            instance = this;
    }

    void Start ()
    {

        safeToMove = true;
        pieces = new GameObject[8, 8];
        white = new Player("white", true);
        black = new Player("black", false);

        if (PlayerPrefs.GetString("From").Equals("NewGame"))
        {
            currentPlayer = white;
            otherPlayer = black;
            Player1.color = Color.yellow;
            Player2.color = Color.cyan;
            Player1View.onClick.Invoke();
            InitialSetup();
        }

        else
        {
            saver = openSaved();
  
            playerTurn = saver.playerTurned;
            if (playerTurn.Equals("white"))
            {
                currentPlayer = white;
                otherPlayer = black;
                Player1.color = Color.yellow;
                Player2.color = Color.cyan;
                Player1View.onClick.Invoke();
            }
            else
            {
                currentPlayer = black;
                otherPlayer = white;
                Player2.color = Color.yellow;
                Player1.color = Color.cyan;
                Player2View.onClick.Invoke();
            }
            ResumeSetup();

        }
    }
    private void ResumeSetup()
    {
        List<SudoPiece> tempList = saver.allThePieces;
        foreach (SudoPiece x in tempList)
        {
            if(x.name.Equals("whiteKnight"))
                AddPiece(whiteKnight, white, x.col, x.row);

            else if (x.name.Equals("whiteRook"))
                AddPiece(whiteRook, white, x.col, x.row);

            else if (x.name.Equals("whiteBishop"))
                AddPiece(whiteBishop, white, x.col, x.row);
           
            else if (x.name.Equals("whiteQueen"))
                AddPiece(whiteQueen, white, x.col, x.row);
           
            else if (x.name.Equals("whiteKing"))
                AddPiece(whiteKing, white, x.col, x.row);

            else if (x.name.Equals("whitePawn"))
                AddPiece(whitePawn, white, x.col, x.row);

            else if (x.name.Equals("blackRook"))
                AddPiece(blackRook, black, x.col, x.row);
          
            else if (x.name.Equals("blackKnight"))
                AddPiece(blackKnight, black, x.col, x.row);
           
            else if (x.name.Equals("blackBishop"))
                AddPiece(blackBishop, black, x.col, x.row);

            else if (x.name.Equals("blackQueen"))
                AddPiece(blackQueen, black, x.col, x.row);

            else if (x.name.Equals("blackKing"))
                AddPiece(blackKing, black, x.col, x.row);

            else if (x.name.Equals("blackPawn"))
                AddPiece(blackPawn, black, x.col, x.row);

        }
    }
    private void InitialSetup()
    {
        
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
        }

    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;

   
    }

    public void RemovePiece(GameObject piece)
    {
        board.RemovePiece(piece);
        //Debug.Log("We Removed A piece");
        if (piece.name.Contains("KingBlack"))
        {
            Debug.Log("Player 1 Wins");
            WinScreen("Player 1");
        }
            

        if (piece.name.Contains("KingWhite"))
        {
            Debug.Log("Player 2 Wins");
            WinScreen("Player 2");
        }
           
        //Debug.Log(currentPlayer.capturedPieces);
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

    public bool DoesPieceBelongToOtherPlayer(GameObject piece)
    {
        return otherPlayer.pieces.Contains(piece);
    }

    //GameManager.instance.DoesPieceBelongToOtherPlayer(GameManager.instance.PieceAtGrid(specificGrid))
    
    public bool DoesPieceOnGridBelongToOther(Vector2Int grid)
    {
        return DoesPieceBelongToOtherPlayer(PieceAtGrid(grid));
    }

    //moves piece to specified grid coordinate
    public IEnumerator Move(GameObject piece, Vector2Int gridPoint)
    {
        safeToMove = false;
        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        //Debug.Log("OLD LOCATION: " + piece.transform.position);



        yield return board.StartCoroutine(board.MovePiece(piece, gridPoint));
        playerMoved.PlayDelayed(0.2f);
        //Debug.Log("Exited board's move function!!!!!!!!!!!!!!!!!!!!!" + " NEW LOCATION: " + piece.transform.position);
        safeToMove = true;
        Player tempPlayer = new Player("temp", true);
        tempPlayer = otherPlayer;
        otherPlayer = currentPlayer;
        currentPlayer = tempPlayer;

        if (currentPlayer == black)
        {
            Player1.color = Color.cyan;
            Player2.color = Color.yellow;
            playerTurn = "black";
            yield return new WaitForSeconds(0.65f);
            Player2View.onClick.Invoke();
        }
        else
        {
            Player1.color = Color.yellow;
            Player2.color = Color.cyan;
            playerTurn = "white";
            yield return new WaitForSeconds(0.65f);
            Player1View.onClick.Invoke();
        }
    
        //Debug.Log("other player holds: " + otherPlayer.name);
        //Debug.Log("current player holds: " + currentPlayer.name);

        //locating king
        //GameObject foundPiece = otherPlayer.pieces.Find(x => (x.GetComponent(typeof(Piece)) as Piece).type == PieceType.King);
        //Debug.Log("Other King is at " + GridForPiece(foundPiece));
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
        List<GameObject> currentPiecesOnField = new List<GameObject>(currentPlayer.pieces);
        currentPiecesOnField.AddRange(otherPlayer.pieces);

        int numOfOtherPieces = currentPiecesOnField.Count;
        Vector2Int gridOtherPiece;

        for (int i = 0; i < numOfOtherPieces; i++)
        {
            gridOtherPiece = GridForPiece(currentPiecesOnField[i]);

            if (possibleMoves.Contains(gridOtherPiece))
            {
                //remove type rook
                if (currentPiece.type == PieceType.Rook) checkRookMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);

                //remove type bishop
                if (currentPiece.type == PieceType.Bishop) checkBishopMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);

                //remove type queen
                if (currentPiece.type == PieceType.Queen)
                {
                    checkBishopMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);
                    checkRookMoves(possibleMoves, gridOtherPiece, gridCurrentPiece);
                }

                if (currentPiece.type == PieceType.Pawn) checkPawnMovesForward(possibleMoves, gridOtherPiece, gridCurrentPiece);

                //remove ally piece
                if (DoesPieceBelongToCurrentPlayer(currentPiecesOnField[i])) possibleMoves.Remove(gridOtherPiece);
            }
        }

        //remove type pawn
        if (currentPiece.type == PieceType.Pawn) checkPawnMovesDiagonal(possibleMoves, gridCurrentPiece);


        //remove all coordinates that are out of bounds
        possibleMoves.RemoveAll(move => move.x > 7 || move.x < 0 || move.y > 7 || move.y < 0);
    }
    public void checkPawnMovesForward(List<Vector2Int> moveList, Vector2Int gridOtherPiece, Vector2Int gridCurrentPiece)
    { 
        if (currentPlayer == white)
            moveList.RemoveAll(move => gridOtherPiece.x == gridCurrentPiece.x && gridOtherPiece.y > gridCurrentPiece.y && move.x == gridCurrentPiece.x && move.y >= gridOtherPiece.y);

        if (currentPlayer == black)
            moveList.RemoveAll(move => gridOtherPiece.x == gridCurrentPiece.x && gridOtherPiece.y < gridCurrentPiece.y && move.x == gridCurrentPiece.x && move.y <= gridOtherPiece.y);
    }

    public void checkPawnMovesDiagonal(List<Vector2Int> moveList, Vector2Int gridCurrentPiece)
    {
        Vector2Int diagonalLeft = new Vector2Int();
        Vector2Int diagonalRight = new Vector2Int();

        if (currentPlayer == white)
        {
            diagonalLeft.x = gridCurrentPiece.x - 1;
            diagonalLeft.y = gridCurrentPiece.y + 1;

            diagonalRight.x = gridCurrentPiece.x + 1;
            diagonalRight.y = gridCurrentPiece.y + 1;

            //if piece at grid does not belong to other player
            if (!DoesPieceBelongToOtherPlayer(PieceAtGrid(diagonalLeft)))
                moveList.Remove(diagonalLeft);

            if (!DoesPieceBelongToOtherPlayer(PieceAtGrid(diagonalRight)))
                moveList.Remove(diagonalRight);
        }
        else if(currentPlayer == black)
        {
            diagonalLeft.x = gridCurrentPiece.x + 1;
            diagonalLeft.y = gridCurrentPiece.y - 1;

            diagonalRight.x = gridCurrentPiece.x - 1;
            diagonalRight.y = gridCurrentPiece.y - 1;

            //if piece at grid does not belong to other player
            if (!DoesPieceBelongToOtherPlayer(PieceAtGrid(diagonalLeft)))
                moveList.Remove(diagonalLeft);

            if (!DoesPieceBelongToOtherPlayer(PieceAtGrid(diagonalRight)))
                moveList.Remove(diagonalRight);
        }
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

    public void WinScreen(String play)
    {
        winPanel.SetActive(true);
        GameoverTxt.SetActive(true);
        if(play == "Player 2")
            WinPlayerName.text = PlayerPrefs.GetString("Player2") + " Wins!";
        else if(play == "Player 1")
            WinPlayerName.text = PlayerPrefs.GetString("Player1") + " Wins!";

        safeToMove = false;
        board.enabled = false;

    }
    public void MultiToList()
    {
        int width = pieces.GetLength(0);
        int height = pieces.GetLength(1);
        GameObject temper;
        String tempname = "";
        String clone = "(Clone)";
        String name = "";
        String color = "";
        allPieces  = new List<GameObject>(width * height);
        saver = new SavedData();
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                if (pieces[i, j] != null)
                {

                    allPieces.Add(pieces[i, j]);
                    temper = pieces[i, j];
                    tempname = temper.name;
                    if (tempname.EndsWith(clone))
                        name = tempname.Remove(tempname.Length - clone.Length);
                    if (name.Contains("White"))
                        color = "white";

                    else if (name.Contains("Black"))
                        color = "black";

                    name = name.Remove(name.Length - 5);
                    name = color + name;
                    SudoPiece sample = new SudoPiece(name, color, i,j);
                    saver.allThePieces.Add(sample); 
                   
                }    
    }
    public String getPlayerTurn()
    {
        return playerTurn;
    }
    public SavedData openSaved()
    {
        try
        {
            using (Stream stream = File.Open("data.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                SavedData loader = (SavedData)bin.Deserialize(stream);
                return loader;
               
            }
        }
        catch (IOException)
        {
            return null;
        }
    }

}

[Serializable()]
public class SudoPiece
{
    public String name;
    public String color;
    public int col;
    public int row;

    public SudoPiece(string a ,string b, int x, int y)
    {
        name = a;
        color = b;
        col = x;
        row = y;

    }
}

[Serializable()]
public class SavedData
{
    public String playerTurned;
    public List<SudoPiece> allThePieces;

    public SavedData()
    {
        try
        {
            playerTurned = GameManager.instance.getPlayerTurn();
        }
        catch(Exception ex){}

        allThePieces = new List<SudoPiece>();
    }




}