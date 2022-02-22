using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces = new List<GameObject>();

    private void Update()
    {
        DrawChessboard();
    }

    private void Start()
    {
        SpawnChesspiece(0, Vector3.zero);
    }

    private void SpawnChesspiece(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessPiecePrefabs[index], position, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activeChessPieces.Add(go);
    }

    //temporary, later on we will use own assets
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
