using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    #region ChessPieceEnumeration

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

    #endregion ChessPieceEnumeration

    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_OFFSET = 0.76243f;
    private const float TILE_SIZE = 1.52486f;
    private bool collide = true;
    private bool pieceCollide = false;

    private const int CHESSBOARD_SIZE = 8;
    private char[,] chessBoard = new char[CHESSBOARD_SIZE, CHESSBOARD_SIZE];

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public List<Rigidbody> ChessRigidBodies;
    public Rigidbody board;

    private void Update()
    {
    }

    private void Start()
    {
        InitialiseArray();
    }

    public void updateArray(Vector3 oldPos, Vector3 newPos)
    {
        Debug.Log("WORKING!");
        //get the indexes
        int oldX = (int) ((oldPos.x - TILE_OFFSET) / TILE_SIZE + 0.5);
        int oldY = (int) ((oldPos.z - TILE_OFFSET) / TILE_SIZE + 0.5);

        int newX = (int) ((newPos.x - TILE_OFFSET) / TILE_SIZE + 0.5);
        int newY = (int) ((newPos.z - TILE_OFFSET) / TILE_SIZE + 0.5);

        chessBoard[newX, newY] = chessBoard[oldX, oldY];
        chessBoard[oldX, oldY] = EMPTY_SPACE;

        //for debugging the array
        /*
        string debug = "";
        for (int i = 0; i < 8; i++)
        {
            debug += "{";
            for (int j = 0; j < 8; j++)
            {
                debug += chessBoard[i, j] + ", ";
            }
            debug += "} \n";
        }
        */
    }

    public void ToggleKinematic()
    {
        foreach (Rigidbody piece in ChessRigidBodies)
            piece.isKinematic = !piece.isKinematic;
    }

    public void ToggleCollider()
    {
        Physics.IgnoreLayerCollision(0, 3, collide);
        collide = !collide;
    }

    public void PieceCollideToggle()
    {
        Physics.IgnoreLayerCollision(3, 3, pieceCollide);
        pieceCollide = !pieceCollide;
    }

    public void tableFlip()
    {
        board.AddExplosionForce(10f, new Vector3(10f, 10f), 2);
    }

    public void InitialiseArray()
    {
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
        chessBoard[3, 0] = WHITE_KING;

        //Queen
        chessBoard[4, 0] = WHITE_QUEEN;

        //Rooks
        chessBoard[0, 0] = WHITE_ROOK;
        chessBoard[7, 0] = WHITE_ROOK;

        //Bishops
        chessBoard[2, 0] = WHITE_BISHOP;
        chessBoard[5, 0] = WHITE_BISHOP;

        //Knights
        chessBoard[1, 0] = WHITE_KNIGHT;
        chessBoard[6, 0] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[i, 1] = WHITE_PAWN;
        }

        //Black chesspieces

        //king
        chessBoard[3, 7] = BLACK_KING;

        //Queen
        chessBoard[4, 7] = BLACK_QUEEN;

        //Rooks
        chessBoard[0, 7] = BLACK_ROOK;
        chessBoard[7, 7] = BLACK_ROOK;

        //Bishops
        chessBoard[2, 7] = BLACK_BISHOP;
        chessBoard[5, 7] = BLACK_BISHOP;

        //Knights
        chessBoard[1, 7] = BLACK_KNIGHT;
        chessBoard[6, 7] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[i, 6] = BLACK_PAWN;
        }
    }
}
