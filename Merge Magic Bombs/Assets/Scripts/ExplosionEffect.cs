using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;

    public CubeColor bombColor;
    public int explosionArea;
    public int explosionDamage;
    public CubeColor expColor;
    public GameObject explosionParticle;
    private float currentTime;

    private GameObject _clone = null;

    void Start()
    {
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        Vector3 currentVec = Vector3.Lerp(Vector3.zero,
            new Vector3(explosionArea, explosionArea, explosionArea), currentTime / _duration);

        transform.localScale = currentVec;

        if (transform.localScale.x >= explosionArea && _clone == null)
        {
            _clone = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            _clone.transform.parent = transform;
        }
    }

}
