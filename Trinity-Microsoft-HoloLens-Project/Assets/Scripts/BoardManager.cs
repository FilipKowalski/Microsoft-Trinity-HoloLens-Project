using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    #region ChessPieceEnumeration

    private int EMPTY_SPACE = -1;
    private int BLACK_KING = 0;
    private int BLACK_QUEEN = 1;
    private int BLACK_ROOK = 2;
    private int BLACK_BISHOP = 3;
    private int BLACK_KNIGHT = 4;
    private int BLACK_PAWN = 5;
    private int WHITE_KING = 6;
    private int WHITE_QUEEN = 7;
    private int WHITE_ROOK = 8;
    private int WHITE_BISHOP = 9;
    private int WHITE_KNIGHT = 10;
    private int WHITE_PAWN = 11;

    #endregion ChessPieceEnumeration

    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_OFFSET = 0.76243f;
    private const float TILE_SIZE = 1.52486f;
    private bool collide = true;

    private const int CHESSBOARD_SIZE = 8;
    private  int[,] chessBoard = new int[CHESSBOARD_SIZE, CHESSBOARD_SIZE];

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
        int OldX = ((oldPos.x / TILE_OFFSET) - 1) as int;
        int Oldy = ((oldPos.y / TILE_OFFSET) - 1) as int;

        int NewX = ((newPos.x / TILE_OFFSET) - 1) as int;
        int Newy = ((newPos.y / TILE_OFFSET) - 1) as int;

        chessBoard[newX]
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

        //Black Chesspieces

        //king
        chessBoard[3, 0] = BLACK_KING;

        //Queen
        chessBoard[4, 0] = BLACK_QUEEN;

        //Rooks
        chessBoard[0, 0] = BLACK_ROOK;
        chessBoard[7, 0] = BLACK_ROOK;

        //Bishops
        chessBoard[2, 0] = BLACK_BISHOP;
        chessBoard[5, 0] = BLACK_BISHOP;

        //Knights
        chessBoard[1, 0] = BLACK_KNIGHT;
        chessBoard[6, 0] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[i, 1] = BLACK_PAWN;
        }

        //White chesspieces

        //king
        chessBoard[3, 7] = WHITE_KING;

        //Queen
        chessBoard[4, 7] = WHITE_QUEEN;

        //Rooks
        chessBoard[0, 7] = WHITE_ROOK;
        chessBoard[7, 7] = WHITE_ROOK;

        //Bishops
        chessBoard[2, 7] = WHITE_BISHOP;
        chessBoard[5, 7] = WHITE_BISHOP;

        //Knights
        chessBoard[1, 7] = WHITE_KNIGHT;
        chessBoard[6, 7] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[i, 6] = WHITE_PAWN;
        }
    }
}
