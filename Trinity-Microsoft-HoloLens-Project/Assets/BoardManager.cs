using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;

    private void Update()
    {
        DrawChessboard();
    }

    //temporary, later on we will use own assets
    private void DrawChessboard()
    {
        // Vector3.right is a unit vector so we multiply by TileSize and by 8 to obtain a vector the is the length of the board
        Vector3 widthLine = (Vector3.right * TILE_SIZE) * 8;
        Vector3 lengthLine = (Vector3.forward * TILE_SIZE) * 8;

        for (int i = 0; i <= 8; i ++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {

            }
        }
    }
}
