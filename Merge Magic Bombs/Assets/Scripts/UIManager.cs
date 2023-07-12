using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _mainCam;
    private GridStateManager _gridStateManager;

    public GameObject bombPanel;
    public GameObject currentObject;


    private void Awake()
    {
        _mainCam = Camera.main;
        _gridStateManager = GetComponent<GridStateManager>();
    }

    private void Update()
    {

    }

    public void UITouched(Vector2 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && _gridStateManager.isListen)
        {
            Collider collider = hit.collider;

            if (collider != null)
            {
                currentObject = collider.gameObject;
                _gridStateManager.UITouched(collider);
            }
        }
    }

    public void ConfirmRandomBomb()
    {
        _gridStateManager.UpdateState(GridActionTypes.PlaceBomb);
    }

    public void RejectRandomBomb()
    {
        _gridStateManager.UpdateState(GridActionTypes.CancelPlaceBomb);
    }
}
