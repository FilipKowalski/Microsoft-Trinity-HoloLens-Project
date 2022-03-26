using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    #region FED Notation

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

    private char AIPlayer = 'w';
    private char WHITE = 'w';
    private char BLACK = 'b';

    private string NEXTCHESSMOVE_OPTIONS = "- - 0 1";

    #endregion FED Notation

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
    public List<Collider> ChessColliders;
    public Rigidbody board;

    private void Update()
    {
    }

    private void Start()
    {
        InitialiseArray();
        Debug.Log(ArrayToForsythEdwards(chessBoard));
    }


    public void UpdateArray(Vector3 oldPos, Vector3 newPos)
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
        Debug.Log(debug);
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
        chessBoard[0, 3] = WHITE_QUEEN;

        //Queen
        chessBoard[0, 4] = WHITE_KING;

        //Rooks
        chessBoard[0, 0] = WHITE_ROOK;
        chessBoard[0, 7] = WHITE_ROOK;

        //Bishops
        chessBoard[0, 2] = WHITE_BISHOP;
        chessBoard[0, 5] = WHITE_BISHOP;

        //Knights
        chessBoard[0, 1] = WHITE_KNIGHT;
        chessBoard[0, 6] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[1, i] = WHITE_PAWN;
        }

        //Black chesspieces

        //king
        chessBoard[7, 3] = BLACK_QUEEN;

        //Queen
        chessBoard[7, 4] = BLACK_KING;

        //Rooks
        chessBoard[7, 0] = BLACK_ROOK;
        chessBoard[7, 7] = BLACK_ROOK;

        //Bishops
        chessBoard[7, 2] = BLACK_BISHOP;
        chessBoard[7, 5] = BLACK_BISHOP;

        //Knights
        chessBoard[7, 1] = BLACK_KNIGHT;
        chessBoard[7, 6] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[6, i] = BLACK_PAWN;
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
        FED += " " + AIPlayer + " " + NEXTCHESSMOVE_OPTIONS;
        return FED;
    }
}
