using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public string txt;
    [SerializeField] private TMP_Text txtObj;
    [SerializeField] private float speed;

    void Start()
    {
        txtObj.GetComponent<TMP_Text>().text = txt;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3.0f);
    }
}
