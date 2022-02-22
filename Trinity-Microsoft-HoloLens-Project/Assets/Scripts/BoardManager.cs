using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;

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

        //spawn black chesspieces

        //king
        SpawnChesspiece(0, GetTileCenter(3, 0));

        //Queen
        SpawnChesspiece(1, GetTileCenter(4, 0));

        //Rooks
        SpawnChesspiece(2, GetTileCenter(0, 0));
        SpawnChesspiece(2, GetTileCenter(7, 0));

        //Bishops
        SpawnChesspiece(3, GetTileCenter(2, 0));
        SpawnChesspiece(3, GetTileCenter(5, 0));

        //Knights
        SpawnChesspiece(4, GetTileCenter(1, 0));
        SpawnChesspiece(4, GetTileCenter(6, 0));

        //Pawns
        for (int i = 0; i < 8; i++)
            SpawnChesspiece(5, GetTileCenter(i, 1));

        //spawn White chesspieces

        //king
        SpawnChesspiece(6, GetTileCenter(3, 7));

        //Queen
        SpawnChesspiece(7, GetTileCenter(4, 7));

        //Rooks
        SpawnChesspiece(8, GetTileCenter(0, 7));
        SpawnChesspiece(8, GetTileCenter(7, 7));

        //Bishops
        SpawnChesspiece(9, GetTileCenter(2, 7));
        SpawnChesspiece(9, GetTileCenter(5, 7));

        //Knights
        SpawnChesspiece(10, GetTileCenter(1, 7));
        SpawnChesspiece(10, GetTileCenter(6, 7));

        //Pawns
        for (int i = 0; i < 8; i++)
            SpawnChesspiece(11, GetTileCenter(i, 6));
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
