using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerManager : MonoBehaviour
{
    BoardManager boardManager;
    Vector3 oldPos = Vector3.zero;
    Vector3 newPos;

    void Awake()
    {
        boardManager = GameObject.Find("GameBoard").GetComponent<BoardManager>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        newPos = trigger.bounds.center;
        if (oldPos != Vector3.zero && oldPos != newPos)
            boardManager.updateArray(oldPos, newPos);
    }

    private void OnTriggerExit(Collider trigger)
    {
        oldPos = newPos;
    }
}
