using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InputManager playerInputManager;
    private Camera mainCam;

    private void Awake()
    {
        playerInputManager = GetComponent<InputManager>();
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
            // Get the collider and its tag
            Collider collider = hit.collider;

            if (collider.tag == "Cell")
            {
                Debug.Log(collider.gameObject.transform.position);
            }
        }
    }
}
