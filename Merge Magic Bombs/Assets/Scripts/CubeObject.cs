using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    public GridStateManager gridStateManager;

    public int cubeStrength;
    public CubeColor cubeColor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explosion" && other.GetComponent<ExplosionEffect>().expColor == cubeColor)
        {
            Damage(other.GetComponent<ExplosionEffect>().explosionDamage);
        }
    }

    public void Damage(int damageAmount)
    {
        cubeStrength -= damageAmount;

        if (cubeStrength <= 0)
        {
            DestroyCube();
        }
    }

    private void DestroyCube()
    {
        gridStateManager.ActivateNewCell(new Vector3(transform.position.x, -0.25f, transform.position.z));

        if (Random.Range(0, 2) == 0)
        {
            gridStateManager.CreateModification(gameObject);
        }

        Destroy(gameObject);
    }
}
