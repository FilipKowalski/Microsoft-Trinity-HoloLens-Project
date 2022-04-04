using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceTriggerManager : MonoBehaviour
{
    BoardManager bm;
    bool triggered = true;


    void Awake()
    {
        bm = GameObject.Find("GameBoard").GetComponent<BoardManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!triggered)
            bm.pieceMoved = this.gameObject;       
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        Debug.Log("EXIT");
    }
}