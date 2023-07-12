using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _mainCam;
    private GridStateManager _gridStateManager;

    public GameObject bombPanel;
    public GameObject blastBtn;
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
        if (!_gridStateManager.isListen)
        {
            return;
        }

        Ray ray = _mainCam.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
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

    public void Blast()
    {
        _gridStateManager.UpdateState(GridActionTypes.Blast);
    }
}
