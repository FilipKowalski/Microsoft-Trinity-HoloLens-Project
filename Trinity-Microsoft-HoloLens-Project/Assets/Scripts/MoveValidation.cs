using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class MoveValidation : MonoBehaviour
{

    public Vector3 oldPos;
    public Vector3 newPos;
    BoardManager bm;

    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.Find("GameBoard").GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOldPos()
    {
        Debug.Log("Set Old Pos");
        oldPos = this.transform.localPosition;
    }

    public void SetNewPos()
    {
        newPos = this.transform.localPosition;
        bm.EndTurn(oldPos, newPos, this.gameObject);
    }
}
