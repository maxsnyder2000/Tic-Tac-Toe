using UnityEngine;
using System.Collections;

public class PieceTravel : MonoBehaviour
{
    public float speed;

    private Vector3 boardSpace;
    private bool startNow = false;
    private bool done;
    private float doneDistance = 0.01f;

    void Update()
    {
        if (startNow && !done)
        {
            float x = Mathf.Lerp(transform.position.x, boardSpace.x, speed);
            float y = Mathf.Lerp(transform.position.y, boardSpace.y, speed);
            float z = Mathf.Lerp(transform.position.z, boardSpace.z, speed);
            transform.position = new Vector3(x, y, z);

            if (Mathf.Abs(transform.position.x - boardSpace.x) < doneDistance &&
                Mathf.Abs(transform.position.y - boardSpace.y) < doneDistance &&
                Mathf.Abs(transform.position.z - boardSpace.z) < doneDistance) done = true;
        }
    }

    public void SetBoardSpace(Vector3 boardSpace)
    {
        this.boardSpace = boardSpace;
    }

    public void StartNow()
    {
        startNow = true;
    }
}
