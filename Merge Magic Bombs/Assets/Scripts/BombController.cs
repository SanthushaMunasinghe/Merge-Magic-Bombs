using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public int damageAmount = 1;
    public int damageArea = 3;
    public CubeColor bombColor;

    public float duration;

    private Vector3 targetPos;
    public Vector3 currentVec;
    private Vector3 currentPos;
    private float currentTime;

    public GameObject parentCell;
    [SerializeField] private GameObject spotMarker;

    public GridStateManager gridStateManager;

    public bool bombSelected = false;
    public bool isMoving = false;

    void Start()
    {
        spotMarker.transform.localScale = new Vector3(damageArea, damageArea, damageArea);
    }

    void Update()
    {
        if (isMoving)
            MovetoPosition();

        if (bombSelected)
            spotMarker.SetActive(true);
        else
            spotMarker.SetActive(false);
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

    public void Blast(GameObject eFX)
    {
        bombSelected = false;
        GameObject eObj = eFX;
        ExplosionEffect efxScript = eObj.GetComponent<ExplosionEffect>();
        efxScript.explosionDamage = damageAmount;
        efxScript.explosionArea = damageArea;
        efxScript.expColor = bombColor;
        GameObject eFXClone = Instantiate(eObj, transform.position, Quaternion.identity);
        eFXClone.transform.parent = transform;
        gridStateManager.availableBombs.Remove(gameObject);
        Destroy(gameObject, 0.5f);
    }
}
