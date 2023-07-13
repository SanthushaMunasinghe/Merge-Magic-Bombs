using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    public GridStateManager gridStateManager;

    public int cubeStrength;
    public CubeColor cubeColor;

    private bool destroyFlag = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (destroyFlag)
        {
            DestroyCube();
        }
    }

    public void Damage(int damageAmount)
    {
        cubeStrength -= damageAmount;
        if (cubeStrength <= 0)
        {
            destroyFlag = true;
        }
    }

    private void DestroyCube()
    {
        gridStateManager.ActivateNewCell(new Vector3(transform.position.x, -0.25f, transform.position.z));

        if (Random.Range(0, 2) == 0)
        {
            gridStateManager.CreateModification(gameObject);
        }
        gridStateManager.spawnedCubes.Remove(gameObject);

        foreach (GameObject expPrefab in gridStateManager.cubeExplosionPrefabs)
        {
            if (expPrefab.GetComponent<CubeExplosionObject>().explColor == cubeColor)
            {
                Instantiate(expPrefab, transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
