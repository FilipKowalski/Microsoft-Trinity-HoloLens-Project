using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerManager : MonoBehaviour
{
    BoardManager boardManager;
    Vector3 oldPos;
    Vector3 newPos;
    bool triggered = true;


    void Awake()
    {
        boardManager = GameObject.Find("GameBoard").GetComponent<BoardManager>();
        oldPos = this.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!triggered)
        {
            boardManager.pieceMoved = this.gameObject;
            triggered = true;
            Debug.Log("ENTER");
            newPos = this.transform.localPosition;
            //Debug.Log(boardManager.pieceMoved.tag);
            boardManager.UpdateArray(oldPos, newPos);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        Debug.Log("EXIT");
        oldPos = other.transform.localPosition;
    }
}