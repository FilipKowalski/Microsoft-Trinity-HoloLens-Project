using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerManager : MonoBehaviour
{
    BoardManager boardManager;
    Vector3 oldPos = Vector3.zero;
    Vector3 newPos;
    int moveCount = 0;


    void Awake()
    {
        boardManager = GameObject.Find("GameBoard").GetComponent<BoardManager>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("ENTER");
        newPos = trigger.transform.localPosition;
        if (oldPos != Vector3.zero && oldPos != newPos)
        {
            if (ValidateMove(oldPos, newPos, boardManager.GetPiece(oldPos)) && boardManager.movesThisTurn < 1)
            {
                //Check if captured and move
                boardManager.UpdateArray(oldPos, newPos);
                moveCount++;
            }
            else
            {
                //TODO move piece back to original position if 
                MovePiece(boardManager.GetPiece(oldPos), oldPos, newPos);
            }
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        Debug.Log("EXIT");
        oldPos = trigger.transform.localPosition;
    }

    private bool ValidateMove(Vector3 oldPos, Vector3 newPos, char Piece)
    {
        //get the indexes
        int oldX = (int)((oldPos.x - boardManager.getTileOffset()) / boardManager.getTileSize() + 0.5);
        int oldY = (int)((oldPos.z - boardManager.getTileOffset()) / boardManager.getTileSize() + 0.5);

        int newX = (int)((newPos.x - boardManager.getTileOffset()) / boardManager.getTileSize() + 0.5);
        int newY = (int)((newPos.z - boardManager.getTileOffset()) / boardManager.getTileSize() + 0.5);

        // private char EMPTY_SPACE = '1';
        // private char BLACK_KING = 'k';
        // private char BLACK_QUEEN = 'q';
        // private char BLACK_ROOK = 'r';
        // private char BLACK_BISHOP = 'b';
        // private char BLACK_KNIGHT = 'n';
        // private char BLACK_PAWN = 'p';
        // private char WHITE_KING = 'K';
        // private char WHITE_QUEEN = 'Q';
        // private char WHITE_ROOK = 'R';
        // private char WHITE_BISHOP = 'B';
        // private char WHITE_KNIGHT = 'N';
        // private char WHITE_PAWN = 'P';
        switch (Piece)
        {
            case 'P':
                if (moveCount == 0 && (newY == oldY - 2 || newY == oldY - 1))
                    return true;
                else if (moveCount > 0 && newY == oldY - 1)
                    return true;
                break;

            case 'N':
                if ((newX == oldX + 1 && (newY == oldY + 2 || newY == oldY - 2)) ||
                    (newX == oldX + 2 && (newY == oldY + 1 || newY == oldY - 1)) ||
                    (newX == oldX - 1 && (newY == oldY - 2 || newY == oldY + 2)) ||
                    (newX == oldX - 2 && (newY == oldY - 1 || newY == oldY + 1)))
                    return true;
                break;

            case 'B':
                for (int i = 1; i <= 8; i++)
                    if ((newX == oldX + i && newY == oldY + i) ||
                       (newX == oldX + i && newY == oldY - i) ||
                       (newX == oldX - i && newY == oldY + i) ||
                       (newX == oldX - i && newY == oldY - i))
                        return true;
                break;

            case 'R':
                for (int i = 1; i <= 8; i++)
                    if ((newX == oldX + i) ||
                       (newX == oldX - i) ||
                       (newY == oldY - i) ||
                       (newY == oldY - i))
                        return true;
                break;

            case 'Q':
                for (int i = 1; i <= 8; i++)
                    if (((newX == oldX + i && newY == oldY + i) ||
                       (newX == oldX + i && newY == oldY - i) ||
                       (newX == oldX - i && newY == oldY + i) ||
                       (newX == oldX - i && newY == oldY - i)) ||
                       ((newX == oldX + i) ||
                       (newX == oldX - i) ||
                       (newY == oldY - i) ||
                       (newY == oldY - i)))
                        return true;
                break;

            case 'K':
                if (((newX == oldX + 1 && newY == oldY + 1) ||
                       (newX == oldX + 1 && newY == oldY - 1) ||
                       (newX == oldX - 1 && newY == oldY + 1) ||
                       (newX == oldX - 1 && newY == oldY - 1)) ||
                       ((newX == oldX + 1) ||
                       (newX == oldX - 1) ||
                       (newY == oldY - 1) ||
                       (newY == oldY - 1)))
                    return true;
                break;

            case 'p':
                if (moveCount == 0 && (newX == oldX + 2 || newX == oldX + 1))
                    return true;
                else if (moveCount > 0 && newX == oldX + 1)
                    return true;
                break;

            case 'n':
                if ((newX == oldX + 1 && (newY == oldY + 2 || newY == oldY - 2)) ||
                    (newX == oldX + 2 && (newY == oldY + 1 || newY == oldY - 1)) ||
                    (newX == oldX - 1 && (newY == oldY - 2 || newY == oldY + 2)) ||
                    (newX == oldX - 2 && (newY == oldY - 1 || newY == oldY + 1)))
                    return true;
                break;

            case 'b':
                for (int i = 1; i <= 8; i++)
                    if ((newX == oldX + i && newY == oldY + i) ||
                       (newX == oldX + i && newY == oldY - i) ||
                       (newX == oldX - i && newY == oldY + i) ||
                       (newX == oldX - i && newY == oldY - i))
                        return true;
                break;

            case 'r':
                for (int i = 1; i <= 8; i++)
                    if ((newX == oldX + i) ||
                       (newX == oldX - i) ||
                       (newY == oldY - i) ||
                       (newY == oldY - i))
                        return true;
                break;

            case 'q':
                for (int i = 1; i <= 8; i++)
                    if (((newX == oldX + i && newY == oldY + i) ||
                       (newX == oldX + i && newY == oldY - i) ||
                       (newX == oldX - i && newY == oldY + i) ||
                       (newX == oldX - i && newY == oldY - i)) ||
                       ((newX == oldX + i) ||
                       (newX == oldX - i) ||
                       (newY == oldY - i) ||
                       (newY == oldY - i)))
                        return true;
                break;

            case 'k':
                if (((newX == oldX + 1 && newY == oldY + 1) ||
                       (newX == oldX + 1 && newY == oldY - 1) ||
                       (newX == oldX - 1 && newY == oldY + 1) ||
                       (newX == oldX - 1 && newY == oldY - 1)) ||
                       ((newX == oldX + 1) ||
                       (newX == oldX - 1) ||
                       (newY == oldY - 1) ||
                       (newY == oldY - 1)))
                    return true;
                break;

        }
           return false;
       
    }

    private void MovePiece(char piece, Vector3 newPos, Vector3 oldPos) {
        int oldX = boardManager.GetDictionary()[move[5]];
        int oldY = Convert.ToInt32(new string(move[6], 1)) - 1;
        int newX = ChessPositionToInt[move[7]];
        int newY = Convert.ToInt32(new string(move[8], 1)) - 1;

        activeChessPieces[((oldX * 8) + oldY)].transform.localPosition += new Vector3((oldY - newY) * TILE_SIZE, 0, (oldX - newX) * TILE_SIZE);
    }
}
