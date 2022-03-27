using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    #region FED Notation / API Calls

    private char EMPTY_SPACE = '1';
    private char BLACK_KING = 'k';
    private char BLACK_QUEEN = 'q';
    private char BLACK_ROOK = 'r';
    private char BLACK_BISHOP = 'b';
    private char BLACK_KNIGHT = 'n';
    private char BLACK_PAWN = 'p';
    private char WHITE_KING = 'K';
    private char WHITE_QUEEN = 'Q';
    private char WHITE_ROOK = 'R';
    private char WHITE_BISHOP = 'B';
    private char WHITE_KNIGHT = 'N';
    private char WHITE_PAWN = 'P';

    private char AIPlayer = 'b';
    private char WHITE = 'w';
    private char BLACK = 'b';

    private string APIurlBest = "https://www.chessdb.cn/cdb.php?action=querybest&board=";
    private string APIurlRandom = "https://www.chessdb.cn/cdb.php?action=querysearch&board=";

    Dictionary<char, int> ChessPositionToInt = new Dictionary<char, int>();

    #endregion FED Notation / API Calls

    private const float TILE_OFFSET = 0.76243f;
    private const float TILE_SIZE = 1.52486f;
    private bool collide = true;
    private bool pieceCollide = false;

    private const int CHESSBOARD_SIZE = 8;
    private char[,] chessBoard = new char[CHESSBOARD_SIZE, CHESSBOARD_SIZE];

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public List<Rigidbody> ChessRigidBodies;
    public List<Collider> ChessColliders;
    public Rigidbody board;

    private void Update()
    {
    }

    private void Start()
    {
        InitialiseArray();
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        ChessPositionToInt.Add('a', 0);
        ChessPositionToInt.Add('b', 1);
        ChessPositionToInt.Add('c', 2);
        ChessPositionToInt.Add('d', 3);
        ChessPositionToInt.Add('e', 4);
        ChessPositionToInt.Add('f', 5);
        ChessPositionToInt.Add('g', 6);
        ChessPositionToInt.Add('h', 7);
    }

    public void UpdateArray(Vector3 oldPos, Vector3 newPos)
    {
        //get the indexes
        int oldX = (int) ((oldPos.x - TILE_OFFSET) / TILE_SIZE + 0.5);
        int oldY = (int) ((oldPos.z - TILE_OFFSET) / TILE_SIZE + 0.5);

        int newX = (int) ((newPos.x - TILE_OFFSET) / TILE_SIZE + 0.5);
        int newY = (int) ((newPos.z - TILE_OFFSET) / TILE_SIZE + 0.5);

        chessBoard[newX, newY] = chessBoard[oldX, oldY];
        chessBoard[oldX, oldY] = EMPTY_SPACE;
    }
    
    public void ToggleKinematic()
    {
        foreach (Rigidbody piece in ChessRigidBodies)
            piece.isKinematic = !piece.isKinematic;
    }

    public void ToggleCollider()
    {
        Physics.IgnoreLayerCollision(0, 0, collide);
        collide = !collide;
    }

    public void PieceCollideToggle()
    {
        foreach (Collider piece1 in ChessColliders)
            foreach (Collider piece2 in ChessColliders)
                Physics.IgnoreCollision(piece1, piece2, pieceCollide);

        pieceCollide = !pieceCollide;
    }

    public void tableFlip()
    {
        board.AddExplosionForce(10f, new Vector3(10f, 10f), 2);
    }

    public void InitialiseArray()
    {
        //point 0, 0 is at the top left side of the board as viewed from in editor
        //initialise array
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                chessBoard[i, j] = EMPTY_SPACE;
            }
        }

        //White Chesspieces

        //king
        chessBoard[0, 3] = BLACK_QUEEN;

        //Queen
        chessBoard[0, 4] = BLACK_KING;

        //Rooks
        chessBoard[0, 0] = BLACK_ROOK;
        chessBoard[0, 7] = BLACK_ROOK;

        //Bishops
        chessBoard[0, 2] = BLACK_BISHOP;
        chessBoard[0, 5] = BLACK_BISHOP;

        //Knights
        chessBoard[0, 1] = BLACK_KNIGHT;
        chessBoard[0, 6] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[1, i] = BLACK_PAWN;
        }

        //Black chesspieces

        //king
        chessBoard[7, 3] = WHITE_QUEEN;

        //Queen
        chessBoard[7, 4] = WHITE_KING;

        //Rooks
        chessBoard[7, 0] = WHITE_ROOK;
        chessBoard[7, 7] = WHITE_ROOK;

        //Bishops
        chessBoard[7, 2] = WHITE_BISHOP;
        chessBoard[7, 5] = WHITE_BISHOP;

        //Knights
        chessBoard[7, 1] = WHITE_KNIGHT;
        chessBoard[7, 6] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[6, i] = WHITE_PAWN;
        }
    }

    private string ArrayToForsythEdwards(char[,] chessBoard)
    {
        string FED = "";
        //game state to FED
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                FED += chessBoard[i, j];
            }
            // we dont want the / after the last line
            if (i < 7) { FED += '/'; }
        }
        //whos turn it is and options Next Chess Move Needs (these dont change)
        FED += " " + AIPlayer;
        return FED;
    }

    public void EndTurn()
    {
        Debug.Log("Start End Turn");
        string fed = ArrayToForsythEdwards(chessBoard);
        WebRequest request;
        WebResponse response;
        Stream receiveStream;
        Encoding encode;
        StreamReader readStream;

        request = WebRequest.Create(APIurlBest + fed);
        request.Method = "GET";
        response = request.GetResponse();
        receiveStream = response.GetResponseStream();
        encode = System.Text.Encoding.GetEncoding("utf-8");
        readStream = new StreamReader(receiveStream, encode);

        string move = readStream.ReadLine();
        Debug.Log(fed);
        if (!String.Equals("nobestmove", move, StringComparison.InvariantCultureIgnoreCase))
        {
            Debug.Log(move);
            int oldX = ChessPositionToInt[move[5]];
            int oldY = Convert.ToInt32(new string(move[6], 1)) - 1;
            int newX = ChessPositionToInt[move[7]];
            int newY = Convert.ToInt32(new string(move[8], 1)) - 1;
            UpdateAndMoveActiveChessPieces(oldX, oldY, newX, newY);
        }
        else
        {
            //TODO 
            //this would be a win condition as there are no moves the ai player can make that
            //result in favourable states
        }
    }

    private void UpdateAndMoveActiveChessPieces(int oldX, int oldY, int newX, int newY)
    {
        //destroy chess piece if the move is a take move
        if (chessBoard[7-newX, newY] != '1')
        {
            Destroy(activeChessPieces[((newX * 8) + newY)]);
            Debug.Log("Hellp");
        }

        //move chessPieceInScene
        activeChessPieces[((oldX * 8) + oldY)].transform.localPosition += new Vector3((oldY - newY) * TILE_SIZE, 0, (oldX - newX) * TILE_SIZE);

        //update the gamestate
        chessBoard[7-newY, newX] = chessBoard[7-oldY, oldX];
        chessBoard[7-oldY, oldX] = '1';

        //move chess piece GameObjects in the 1d array
        activeChessPieces[((newX * 8) + newY)] = activeChessPieces[((oldX * 8) + oldY)];
    }
}
