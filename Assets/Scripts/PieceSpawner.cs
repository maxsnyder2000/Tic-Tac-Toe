using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour
{
    public GameObject oPiece;
    public GameObject xPiece;

    private WinTracker winTracker;
    private GameObject[] spaces;
    private int count = 0;
    private bool oTurn = false;
    private bool tied = false;


    void Start()
    {
        winTracker = GetComponent<WinTracker>();
        spaces = GameObject.FindGameObjectsWithTag("Space");
    }

    void Update()
    {
        if (count < 9 && winTracker.GetWon() == WinTracker.Status.None && Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
        else if (count == 9 && winTracker.GetWon() == WinTracker.Status.None && !tied)
        {
            tied = true;
            winTracker.Win(WinTracker.Status.Tie);
        }
    }

    public void Spawn()
    {
        GameObject piece;
        if (oTurn) piece = (GameObject) Instantiate(oPiece, Camera.main.transform.position, Quaternion.identity, transform);
        else       piece = (GameObject) Instantiate(xPiece, Camera.main.transform.position, Quaternion.identity, transform);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;

        Vector3 closestVector = new Vector3();
        float closestDistance = float.PositiveInfinity;

        foreach (GameObject space in spaces)
        {
            if (space != null)
            {
                float currentDistance = Vector3.Distance(Camera.main.ScreenToWorldPoint(mousePosition), space.transform.position);
                if (currentDistance < closestDistance)
                {
                    closestVector = space.transform.position;
                    closestDistance = currentDistance;
                }
            }
        }

        int spaceNumber = 0;
        for (int i = 0; i < spaces.Length; i++)
        {
            if (spaces[i] != null && spaces[i].transform.position.Equals(closestVector))
            {
                spaceNumber = int.Parse(spaces[i].name);
                spaces[i] = null;
                break;
            }
        }

        PieceTravel pieceTravel = piece.GetComponent<PieceTravel>();
        pieceTravel.SetBoardSpace(closestVector);
        pieceTravel.StartNow();

        winTracker.Move(spaceNumber, oTurn);

        oTurn = !oTurn;
        count += 1;
    }
}
