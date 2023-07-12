using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombModification : MonoBehaviour
{
    public ModificationType modType;
    public CubeColor modColor;
    public GameObject parentCell;

    public GridStateManager gridStateManager;

    public float duration;

    private Vector3 targetPos;
    public Vector3 currentVec;
    private Vector3 currentPos;
    private float currentTime;

    public bool modSelected = false;
    public bool isMoving = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isMoving)
            MovetoPosition();
    }

    public void triggerMoveToPosition(Vector3 pos)
    {
        targetPos = pos;
        currentPos = transform.position;
        currentTime = 0.0f;
        isMoving = true;
    }

    private void MovetoPosition()
    {

        currentTime += Time.deltaTime;

        currentVec = Vector3.Lerp(new Vector3(currentPos.x, 0.5f, currentPos.z),
            new Vector3(targetPos.x, 0.5f, targetPos.z), currentTime / duration);

        transform.position = currentVec;

        if (targetPos == transform.position)
        {
            isMoving = false;
        }
    }
}
