using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;
    private bool collide = true;

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public List<Rigidbody> ChessRigidBodies;
    public List<MeshCollider> ChessColliders;

    private void Update()
    {
    }

    private void Start()
    {
    }

    private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 tileCenter = Vector3.zero;
        tileCenter.x += (TILE_SIZE * x) + TILE_OFFSET;
        tileCenter.z += (TILE_SIZE * z) + TILE_OFFSET; 
        return tileCenter;
;    }



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
}
