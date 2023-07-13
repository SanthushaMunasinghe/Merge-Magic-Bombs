using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public int damageAmount = 1;
    public int damageArea = 3;
    public int blastCount = 1;
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
        
    }

    void Update()
    {
        if (isMoving)
            MovetoPosition();

        if (bombSelected)
        {
            spotMarker.SetActive(true);
            spotMarker.transform.localScale = new Vector3(damageArea, damageArea, damageArea);
        }
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

    public bool CheckAvailability(ModificationType type, CubeColor currentColor)
    {
        if (currentColor == bombColor)
        {
            if (type == ModificationType.modtype01 && damageAmount < 3)
                return true;
            else if (type == ModificationType.modtype02 && blastCount < 3)
                return true;
            else if (type == ModificationType.modtype03 && damageArea < 7)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public void AttachModification(GameObject mod, ModificationType type)
    {
        if (type == ModificationType.modtype01)
        {
            StartCoroutine(AttachOrRemove(mod, 1, damageArea));
            damageAmount++;
        }
        else if (type == ModificationType.modtype02)
        {
            StartCoroutine(AttachOrRemove(mod, 1, blastCount));
            blastCount++;
        }
        else if (type == ModificationType.modtype03)
        {
            StartCoroutine(AttachOrRemove(mod, 3, damageArea));
            damageArea += 2;
        }
    }

    private IEnumerator AttachOrRemove(GameObject obj, int expectedValue, int currentValue)
    {
        if (currentValue > expectedValue)
        {
            while (obj.transform.position != transform.position)
            {
                yield return null;
            }

            Destroy(obj);
        }
        else
        {
            obj.GetComponent<Collider>().enabled = false;
            obj.transform.parent = transform;
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
