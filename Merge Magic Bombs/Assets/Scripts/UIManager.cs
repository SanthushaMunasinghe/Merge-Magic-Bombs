using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {

    }

    public void UITouched(Vector2 pos)
    {
        Ray ray = mainCam.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Collider collider = hit.collider;

            if (collider.tag == "Cell")
            {
                Debug.Log(collider.gameObject.transform.position);
            }
        }
    }
}
