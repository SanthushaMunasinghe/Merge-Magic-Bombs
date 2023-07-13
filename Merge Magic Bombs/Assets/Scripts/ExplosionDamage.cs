using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int DamageArea;
    public int DamageAmount;
    public CubeColor expColor;

    public void EnabledMethod(int area, int amount, CubeColor color)
    {
        DamageArea = area;
        DamageAmount = amount;
        expColor = color;
        transform.localScale = new Vector3(DamageArea, DamageArea, DamageArea);

        DamageCubes();
    }

    private void DamageCubes()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageArea/2 + 0.5f);
        foreach (Collider collider in colliders)
        {
            CubeObject cubeScript = collider.GetComponent<CubeObject>();

            if (collider.CompareTag("Cube") && cubeScript.cubeColor == expColor)
            {
                if (cubeScript != null)
                {
                    cubeScript.Damage(DamageAmount);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DamageArea / 2 + 0.5f);
    }
}
