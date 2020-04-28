using UnityEngine;
using System.Collections;

public class WinTracker : MonoBehaviour
{
    public GameObject oWinMessage;
    public GameObject xWinMessage;
    public GameObject tieMessage;
    public GameObject dest;

    public enum Status { None, O, X, Tie };

    private Status won = Status.None;
    private int[,] wins;
    private Status[] squares;

    void Start()
    {
        wins = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 },
            { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } };
        squares = new Status[9];
    }

    public void Move(int square, bool oTurn)
    {
        if (oTurn) squares[square - 1] = Status.O;
        else       squares[square - 1] = Status.X;

        CheckWin();
    }

    public void Win(Status win)
    {
        won = win;

        PieceTravel pieceTravel;
        if (win == Status.O) pieceTravel = oWinMessage.GetComponent<PieceTravel>();
        else if (win == Status.X) pieceTravel = xWinMessage.GetComponent<PieceTravel>();
        else pieceTravel = tieMessage.GetComponent<PieceTravel>();

        pieceTravel.SetBoardSpace(dest.transform.position);
        pieceTravel.StartNow();
    }

    public Status GetWon()
    {
        return won;
    }

    private void CheckWin()
    {
        for (int i = 0; i < wins.Length; i += 3)
        {
            int space1 = wins[i / 3, 0] - 1;
            int space2 = wins[i / 3, 1] - 1;
            int space3 = wins[i / 3, 2] - 1;

            if (squares[space1] != 0 && squares[space1] == squares[space2] && squares[space2] == squares[space3]) Win(squares[space1]);
        }
    }
}
