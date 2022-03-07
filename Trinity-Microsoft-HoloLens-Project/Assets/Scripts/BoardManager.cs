using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    //if one unit is 1 metre lets make the tiles 8cms for now
    private const float TILE_SIZE = 0.08f;
    private const float TILE_OFFSET = 0.04f;
    private bool collide = true;

    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public List<Rigidbody> ChessRigidBodies;
    public Rigidbody board;
    bool flipped = false;

    private void Update()
    {
    }

    private void Start()
    {
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
        if (!flipped)
        {
            board.isKinematic = false;
            board.AddForce(new Vector3(0f, 1000f), ForceMode.Impulse);
            flipped = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            flipped = false;
        }
    }
}
