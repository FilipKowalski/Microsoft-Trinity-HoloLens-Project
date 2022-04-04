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

    public async void SetNewPos()
    {
        newPos = this.transform.localPosition;

        int oldX = (int)((oldPos.x - BoardManager.TILE_OFFSET) / BoardManager.TILE_SIZE + 0.5);
        int oldY = (int)((oldPos.z - BoardManager.TILE_OFFSET) / BoardManager.TILE_SIZE + 0.5);
        int newX = (int)((newPos.x - BoardManager.TILE_OFFSET) / BoardManager.TILE_SIZE + 0.5);
        int newY = (int)((newPos.z - BoardManager.TILE_OFFSET) / BoardManager.TILE_SIZE + 0.5);

        GameObject oldTrigger = bm.triggers2D[oldX, oldY];
        GameObject newTrigger = bm.triggers2D[newX, newY];

        this.transform.localPosition = newTrigger.transform.localPosition;
        transform.rotation = Quaternion.identity;

        bm.UpdateArray(oldX, oldY, newX, newY);

        string test = bm.ArrayToForsythEdwards(BoardManager.chessBoard);
        Debug.Log(test);

        WebRequest request;
        WebResponse response;
        Stream receiveStream;
        Encoding encode;
        StreamReader readStream;

        request = WebRequest.Create(bm.APIurlBest + test);
        request.Method = "GET";
        response = await request.GetResponseAsync();
        receiveStream = response.GetResponseStream();
        encode = System.Text.Encoding.GetEncoding("utf-8");
        readStream = new StreamReader(receiveStream, encode);

        string move = readStream.ReadLine();
        Debug.Log(move);
        if (String.Equals("unknown", move, StringComparison.InvariantCultureIgnoreCase))
        {
            this.transform.localPosition = oldTrigger.transform.localPosition;
            this.transform.rotation = Quaternion.identity;
            BoardManager.chessBoard[oldX, oldY] = BoardManager.chessBoard[newX, newY];
            BoardManager.chessBoard[newX, newY] = bm.EMPTY_SPACE;
        }
        else
        {
            bm.EndTurn();
        }
    }
}
