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
        Debug.Log("ENTER");
        newPos = trigger.transform.localPosition;
        if (oldPos != Vector3.zero && oldPos != newPos)
            boardManager.UpdateArray(oldPos, newPos);
    }

    private void OnTriggerExit(Collider trigger)
    {
        Debug.Log("EXIT");
        oldPos = trigger.transform.localPosition;
    }
}
