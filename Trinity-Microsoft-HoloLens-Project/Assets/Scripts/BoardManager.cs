using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;

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

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public int[,] chessPiecesBoard;

    private void Update()
    {
        DrawChessboard();
    }

    private void Start()
    {
        SpawnAllChesspieces();
    }

    private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 tileCenter = Vector3.zero;
        tileCenter.x += (TILE_SIZE * x) + TILE_OFFSET;
        tileCenter.z += (TILE_SIZE * z) + TILE_OFFSET; 
        return tileCenter;
;    }

    private void SpawnChesspiece(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessPiecePrefabs[index], position, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activeChessPieces.Add(go);
    }

    private void SpawnAllChesspieces()
    {
        activeChessPieces = new List<GameObject>();
        chessPiecesBoard = new int[8,8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                chessPiecesBoard[i,j] = -1;
            }
        }

        //spawn black chesspieces

        //king
        SpawnChesspiece(0, GetTileCenter(3, 0));
        chessPiecesBoard[3, 0] = BLACK_KING;

        //Queen
        SpawnChesspiece(1, GetTileCenter(4, 0));
        chessPiecesBoard[4, 0] = BLACK_QUEEN;

        //Rooks
        SpawnChesspiece(2, GetTileCenter(0, 0));
        chessPiecesBoard[0, 0] = BLACK_ROOK;
        SpawnChesspiece(2, GetTileCenter(7, 0));
        chessPiecesBoard[7, 0] = BLACK_ROOK;

        //Bishops
        SpawnChesspiece(3, GetTileCenter(2, 0));
        chessPiecesBoard[2, 0] = BLACK_BISHOP;
        SpawnChesspiece(3, GetTileCenter(5, 0));
        chessPiecesBoard[5, 0] = BLACK_BISHOP;

        //Knights
        SpawnChesspiece(4, GetTileCenter(1, 0));
        chessPiecesBoard[1, 0] = BLACK_KNIGHT;
        SpawnChesspiece(4, GetTileCenter(6, 0));
        chessPiecesBoard[6, 0] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChesspiece(5, GetTileCenter(i, 1));
            chessPiecesBoard[i, 1] = BLACK_PAWN;
        }

        //spawn White chesspieces

        //king
        SpawnChesspiece(6, GetTileCenter(3, 7));
        chessPiecesBoard[3, 7] = WHITE_KING;

        //Queen
        SpawnChesspiece(7, GetTileCenter(4, 7));
        chessPiecesBoard[4, 7] = WHITE_QUEEN;

        //Rooks
        SpawnChesspiece(8, GetTileCenter(0, 7));
        chessPiecesBoard[0, 7] = WHITE_ROOK;
        SpawnChesspiece(8, GetTileCenter(7, 7));
        chessPiecesBoard[7, 7] = WHITE_ROOK;

        //Bishops
        SpawnChesspiece(9, GetTileCenter(2, 7));
        chessPiecesBoard[2, 7] = WHITE_BISHOP;
        SpawnChesspiece(9, GetTileCenter(5, 7));
        chessPiecesBoard[5, 7] = WHITE_BISHOP;

        //Knights
        SpawnChesspiece(10, GetTileCenter(1, 7));
        chessPiecesBoard[1, 7] = WHITE_KNIGHT;
        SpawnChesspiece(10, GetTileCenter(6, 7));
        chessPiecesBoard[6, 7] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChesspiece(11, GetTileCenter(i, 6));
            chessPiecesBoard[i, 6] = WHITE_PAWN;
        }
    }

    //temporary, later on we will use own assets
    //enable gizmos in editor to see
    private void DrawChessboard()
    {
        // Vector3.right is a unit vector so we multiply by TileSize and by 8 to obtain a vector the is the length of the board
        Vector3 widthLine = (Vector3.right * TILE_SIZE) * 8;
        Vector3 lengthLine = (Vector3.forward * TILE_SIZE) * 8;

        for (float i = 0; i <= TILE_SIZE * 8; i += TILE_SIZE)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (float j = 0; j <= TILE_SIZE * 8; j += TILE_SIZE)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + lengthLine);
            }
        }
    }
}
