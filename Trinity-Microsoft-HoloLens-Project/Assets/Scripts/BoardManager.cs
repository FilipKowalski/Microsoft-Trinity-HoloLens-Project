using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{

    #region FED Notation / API Calls

    public char EMPTY_SPACE = '1';
    private char BLACK_KING = 'k';
    private char BLACK_QUEEN = 'q';
    private char BLACK_ROOK = 'r';
    private char BLACK_BISHOP = 'b';
    private char BLACK_KNIGHT = 'n';
    private char BLACK_PAWN = 'p';
    private char WHITE_KING = 'K';
    private char WHITE_QUEEN = 'Q';
    private char WHITE_ROOK = 'R';
    private char WHITE_BISHOP = 'B';
    private char WHITE_KNIGHT = 'N';
    private char WHITE_PAWN = 'P';

    private char AIPlayer = 'b';

    public string APIurlBest = "https://www.chessdb.cn/cdb.php?action=queryall&board=";
    Encoding encode;
    Dictionary<char, int> ChessPositionToInt = new Dictionary<char, int>();

    #endregion FED Notation / API Calls

    public const float TILE_OFFSET = 0.76243f;
    public const float TILE_SIZE = 1.52486f;
    private bool collide = true;

    public const int CHESSBOARD_SIZE = 8;
    public char[,] chessBoard = new char[CHESSBOARD_SIZE, CHESSBOARD_SIZE];
    public GameObject[,] gameObjects = new GameObject[CHESSBOARD_SIZE, CHESSBOARD_SIZE];
    public List<GameObject> chessPiecePrefabs;
    public List<GameObject> activeChessPieces;
    public List<Rigidbody> ChessRigidBodies;
    public List<Collider> ChessColliders;
    public Rigidbody board;

    public GameObject pieceTaken;
    public GameObject empty;
    public List<GameObject> triggers;
    public static GameObject[,] triggers2D;
    public int[] pos;

    
    private void Update()
    {
    }

    private void Start()
    {
        InitialiseArray();
        CreateDictionary();
        PieceCollideToggle();
        encode = System.Text.Encoding.GetEncoding("utf-8");
    }

    public void InitialiseArray()
    {

        //point 0, 0 is at the top left side of the board as viewed from in editor
        //initialise array
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                chessBoard[i, j] = EMPTY_SPACE;
            }
        }

        //White Chesspieces

        //king
        chessBoard[0, 3] = BLACK_QUEEN;

        //Queen
        chessBoard[0, 4] = BLACK_KING;

        //Rooks
        chessBoard[0, 0] = BLACK_ROOK;
        chessBoard[0, 7] = BLACK_ROOK;

        //Bishops
        chessBoard[0, 2] = BLACK_BISHOP;
        chessBoard[0, 5] = BLACK_BISHOP;

        //Knights
        chessBoard[0, 1] = BLACK_KNIGHT;
        chessBoard[0, 6] = BLACK_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[1, i] = BLACK_PAWN;
        }

        //Black chesspieces

        //king
        chessBoard[7, 3] = WHITE_QUEEN;

        //Queen
        chessBoard[7, 4] = WHITE_KING;

        //Rooks
        chessBoard[7, 0] = WHITE_ROOK;
        chessBoard[7, 7] = WHITE_ROOK;

        //Bishops
        chessBoard[7, 2] = WHITE_BISHOP;
        chessBoard[7, 5] = WHITE_BISHOP;

        //Knights
        chessBoard[7, 1] = WHITE_KNIGHT;
        chessBoard[7, 6] = WHITE_KNIGHT;

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            chessBoard[6, i] = WHITE_PAWN;
        }

        int columnCount = 0;
        int rowCount = 7;

        for (int i = 0; i < ((CHESSBOARD_SIZE * CHESSBOARD_SIZE) - 1); i++)
        {
            if (i % 8 == 0 && i != 0)
            {
                columnCount++;
                rowCount = 7;
            }
                if (activeChessPieces[i] != null)
                    gameObjects[rowCount--, columnCount] = activeChessPieces[i];
                else
                    gameObjects[rowCount--, columnCount] = empty;
        }

        triggers2D = new GameObject[CHESSBOARD_SIZE, CHESSBOARD_SIZE];

        int count = 0;
        for (int i = 0; i < CHESSBOARD_SIZE; i++)
            for (int j = 0; j < CHESSBOARD_SIZE; j++)
                triggers2D[i, j] = triggers[count++];
    }

    private void CreateDictionary()
    {
        ChessPositionToInt.Add('a', 0);
        ChessPositionToInt.Add('b', 1);
        ChessPositionToInt.Add('c', 2);
        ChessPositionToInt.Add('d', 3);
        ChessPositionToInt.Add('e', 4);
        ChessPositionToInt.Add('f', 5);
        ChessPositionToInt.Add('g', 6);
        ChessPositionToInt.Add('h', 7);
    }

    public void UpdateArray(int oldX, int oldY, int newX, int newY, bool getTaken)
    {
        //Debug.Log("Updating Array");

        if (chessBoard[newX, newY] != 1 && getTaken)
            pieceTaken = gameObjects[newX, newY];

        chessBoard[newX, newY] = chessBoard[oldX, oldY];
        chessBoard[oldX, oldY] = EMPTY_SPACE;

        gameObjects[newX, newY] = gameObjects[oldX, oldY];
        gameObjects[oldX, oldY] = empty;

    }

    public async void EndTurn(Vector3 oldPos, Vector3 newPos, GameObject piece)
    {
        //Debug.Log("Start End Turn");
        int oldX = (int)Math.Floor((oldPos.x - TILE_OFFSET) / TILE_SIZE + 0.5) + 4;
        int oldY = (int)Math.Floor((oldPos.z - TILE_OFFSET) / TILE_SIZE + 0.5) + 4;
        int newX = (int)Math.Floor((newPos.x - TILE_OFFSET) / TILE_SIZE + 0.5) + 4;
        int newY = (int)Math.Floor((newPos.z - TILE_OFFSET) / TILE_SIZE + 0.5) + 4;

        GameObject oldTrigger = triggers2D[oldX, oldY];
        GameObject newTrigger = triggers2D[newX, newY];

        if (newX == oldX && newY == oldY)
        {
            Debug.Log("no Move");
            piece.transform.position = oldTrigger.transform.position;
            piece.transform.rotation = Quaternion.identity;
            return;
        }

        piece.transform.position = newTrigger.transform.position;
        piece.transform.rotation = Quaternion.identity;

        UpdateArray(oldX, oldY, newX, newY, true);

        string test = ArrayToForsythEdwards(chessBoard);
        Debug.Log(test);

        WebRequest request;
        WebResponse response;
        Stream receiveStream;        
        StreamReader readStream;

        request = WebRequest.Create(APIurlBest + test);
        request.Method = "GET";
        response = await request.GetResponseAsync();
        receiveStream = response.GetResponseStream();
        
        readStream = new StreamReader(receiveStream, encode);
        
        string move = readStream.ReadLine();
        Debug.Log(move);
        if (String.Equals("unknown", move, StringComparison.InvariantCultureIgnoreCase))
        {
            piece.transform.position = oldTrigger.transform.position;
            piece.transform.rotation = Quaternion.identity;
            UpdateArray(newX, newY, oldX, oldY, false);
        }
        else
        {
            int oldAIX = ChessPositionToInt[move[5]];
            int oldAIY = Convert.ToInt32(new string(move[6], 1)) - 1;
            int newAIX = ChessPositionToInt[move[7]];
            int newAIY = Convert.ToInt32(new string(move[8], 1)) - 1;
            UpdateAndMoveActiveChessPieces(oldAIX, oldAIY, newAIX, newAIY);
        }
        readStream.Close();
    }

    public void ToggleKinematic()
    {
        foreach (Rigidbody piece in ChessRigidBodies)
            piece.isKinematic = !piece.isKinematic;
    }

    public void ToggleCollider()
    {
        Physics.IgnoreLayerCollision(0, 0, collide);
        collide = !collide;
    }

    public void PieceCollideToggle()
    {
        foreach (Collider piece1 in ChessColliders)
            foreach (Collider piece2 in ChessColliders)
                Physics.IgnoreCollision(piece1, piece2, true);
    }

    public string ArrayToForsythEdwards(char[,] chessBoard)
    {
        string FED = "";
        //game state to FED
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                FED += chessBoard[i, j];
            }
            // we dont want the / after the last line
            if (i < 7) { FED += '/'; }
        }
        //whos turn it is and options Next Chess Move Needs (these dont change)
        FED += " " + AIPlayer;
        return FED;
    }

    private void UpdateAndMoveActiveChessPieces(int oldX, int oldY, int newX, int newY)
    {
        //destroy chess piece if the move is a take move
        if (chessBoard[7-newX, newY] != '1')
        {
            Destroy(activeChessPieces[((newX * 8) + newY)]);
            //Debug.Log("Help");
        }

        //move chessPieceInScene
        activeChessPieces[((oldX * 8) + oldY)].transform.localPosition += new Vector3((oldY - newY) * TILE_SIZE, 0, (newX - oldX) * TILE_SIZE);

        //update the gamestate
        chessBoard[7-newY, newX] = chessBoard[7-oldY, oldX];
        chessBoard[7-oldY, oldX] = EMPTY_SPACE;

        //move chess piece GameObjects in the 1d array
        activeChessPieces[((newX * 8) + newY)] = activeChessPieces[((oldX * 8) + oldY)];
    }
}
